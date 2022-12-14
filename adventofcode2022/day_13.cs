using AdventOfCSharp;
using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DayThirteen
    {
        public void Execute()
        {
            Queue<string> signalInput = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_13.txt")).ToList());

            Dictionary<int, bool> IndicesInOrder = new Dictionary<int, bool>();
            List<List<object>> allInput = new List<List<object>>();

            List<object> dividerPacket1 = MakeListFromString("[[2]]");
            List<object> dividerPacket2 = MakeListFromString("[[6]]");

            allInput.Add(dividerPacket1);
            allInput.Add(dividerPacket2);

            int index = 1;

            while (signalInput.Count >= 2)
            {
                string left = signalInput.Dequeue();
                string right = signalInput.Dequeue();
                if (signalInput.Count > 0)
                    signalInput.Dequeue();

                List<object> leftList = MakeListFromString(left);
                List<object> rightList = MakeListFromString(right);

                allInput.Add(new List<object>(leftList));
                allInput.Add(new List<object>(rightList));

                IndicesInOrder.Add(index++, IsInOrder(leftList, rightList));
            }

            Console.WriteLine("Problem 1: {0}", IndicesInOrder.Where(kvp => kvp.Value == true).Sum(kvp => kvp.Key));

            allInput.Sort(IsInOrderForSort);
            allInput.Reverse();

            int index1 = allInput.IndexOf(dividerPacket1);
            int index2 = allInput.IndexOf(dividerPacket2);

            Console.WriteLine("Problem 2: {0}", (index1 + 1) * (index2 + 1));
        }


        // Future self, maybe do this with JSON instead? 
        private List<object> MakeListFromString(string input)
        {
            var list = new List<object>();

            char[] chars = input.ToCharArray();

            for (int i = 1; i < chars.Length; i++)
            {
                if (chars[i].Equals(','))
                {
                    continue;
                }

                if (char.IsDigit(chars[i]))
                {
                    StringBuilder sub = new StringBuilder();

                    while (i < chars.Length && !chars[i].Equals(',') && !chars[i].Equals(']'))
                    {
                        sub.Append(chars[i++]);
                    }

                    list.Add(Convert.ToInt32(sub.ToString()));
                }

                if (i < chars.Length && chars[i].Equals('['))
                {
                    int nestedCount = 1;
                    StringBuilder sub = new StringBuilder();
                    sub.Append(chars[i++]);

                    while (nestedCount > 0)
                    {
                        if (chars[i] == '[')
                            nestedCount++;
                        else if (chars[i] == ']')
                            nestedCount--;

                        sub.Append(chars[i++]);
                    }

                    list.Add(MakeListFromString(sub.ToString()));
                }
            }

            return list;
        }


        private int IsInOrderForSort(List<object> left, List<object> right)
        {
            if (left == null)
            {
                if (right == null)
                    return 0;
                else
                {
                    return -1;
                }
            }
            else
            {
                if (right == null)
                    return 1;
                else
                {
                    if (left.SequenceEqual(right))
                    {
                        return 0;
                    }

                    return IsInOrder(left, right) ? 1 : -1;
                }
            }
        }

        // Future self, if you ever decide to refactor this code, returning a -1, 0, 1 is probably better than this boolean/continue jankiness
        private bool IsInOrder(List<object> left, List<object> right)
        {
            int index = 0;

            while (left.Count > index || right.Count > index)
            {
                if (left.Count == index && right.Count > index)
                {
                    return true;
                }
                object currentLeft = left[index];
                
                if (right.Count == index)
                {
                    return false;
                }
                object currentRight = right[index];

                if (currentLeft.GetType() == currentRight.GetType())
                {
                    if (currentLeft.GetType() == typeof(int))
                    {
                        if ((int)currentLeft == (int)currentRight)
                        {
                            index++;
                            continue;
                        }
                        else if ((int)currentLeft > (int)currentRight)
                            return false;
                        else
                            return true;
                    }
                    else if (currentLeft.GetType() == typeof(List<object>))
                    {
                        if (((List<object>)currentLeft).SequenceEqual((List<object>)currentRight))
                        {
                            index++;
                            continue;
                        }
                        else if (!IsInOrder((List<object>)currentLeft, (List<object>)currentRight))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        index++;
                        continue;
                    }
                }
                else
                {
                    if (currentLeft.GetType() == typeof(int) && currentRight.GetType() == typeof(List<object>))
                    {
                        List<object> newLeft = new List<object>();
                        newLeft.Add((int)currentLeft);

                        if (newLeft.SequenceEqual((List<object>)currentRight))
                        {
                            index++;
                            continue;
                        }
                        else if (!IsInOrder(newLeft, (List<object>)currentRight))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (currentLeft.GetType() == typeof(List<object>) && currentRight.GetType() == typeof(int))
                    {
                        List<object> newRight = new List<object>();
                        newRight.Add((int)currentRight);

                        if (((List<object>)currentLeft).SequenceEqual(newRight))
                        {
                            index++;
                            continue;
                        }
                        else if (!IsInOrder((List<object>)currentLeft, newRight))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        index++;
                        continue;
                    }
                }
            }

            return true;
        }
    }
}
