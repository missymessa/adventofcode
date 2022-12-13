using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayThirteen
    {
        public static void Execute()
        {
            Queue<string> signalInput = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_13.txt")).ToList());

            Dictionary<int, bool> IndicesInOrder = new Dictionary<int, bool>();
            int index = 1;

            while(signalInput.Count >= 2)
            {
                string left = signalInput.Dequeue();
                string right = signalInput.Dequeue();
                if(signalInput.Count > 0) 
                    signalInput.Dequeue();

                Queue<object> leftList = MakeQueueFromString(left);
                Queue<object> rightList = MakeQueueFromString(right);

                IndicesInOrder.Add(index++, IsInOrder(leftList, rightList));
            }

            Console.WriteLine("Problem 1: {0}", IndicesInOrder.Where(kvp => kvp.Value == true).Sum(kvp => kvp.Key));
        }

        private static Queue<object> MakeQueueFromString(string input)
        {
            var list = new Queue<object>();

            char[] chars = input.ToCharArray();

            for(int i = 1; i < chars.Length; i++)
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

                    list.Enqueue(Convert.ToInt32(sub.ToString()));
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

                    //sub.Append(chars[i++]);

                    list.Enqueue(MakeQueueFromString(sub.ToString()));
                }
            }

            return list;
        }


        // Future self, if you ever decide to refactor this code, returning a -1, 0, 1 is probably better than this boolean/continue jankiness
        private static bool IsInOrder(Queue<object> left, Queue<object> right)
        {
            while(left.Count > 0 || right.Count > 0)
            {
                object currentLeft;
                if(!left.TryDequeue(out currentLeft) && right.Count >= 0)
                {
                    return true;
                }

                object currentRight;
                if(!right.TryDequeue(out currentRight))
                {
                    return false;
                }                

                if (currentLeft.GetType() == currentRight.GetType())
                {
                    if(currentLeft.GetType() == typeof(int))
                    {
                        if ((int)currentLeft == (int)currentRight)
                            continue;
                        else if ((int)currentLeft > (int)currentRight)
                            return false;
                        else
                            return true;
                    }
                    else if(currentLeft.GetType() == typeof(Queue<object>))
                    {
                        if(((Queue<object>)currentLeft).SequenceEqual((Queue<object>)currentRight))
                        {
                            continue;
                        }
                        else if (!IsInOrder((Queue<object>)currentLeft, (Queue<object>)currentRight))
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
                        continue;
                    }
                }
                else
                {
                    if (currentLeft.GetType() == typeof(int) && currentRight.GetType() == typeof(Queue<object>)) 
                    {
                        Queue<object> newLeft = new Queue<object>();
                        newLeft.Enqueue((int)currentLeft);

                        if (newLeft.SequenceEqual((Queue<object>)currentRight))
                        {
                            continue;
                        }
                        else if (!IsInOrder(newLeft, (Queue<object>)currentRight))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if(currentLeft.GetType() == typeof(Queue<object>) && currentRight.GetType() == typeof(int))
                    {
                        Queue<object> newRight = new Queue<object>();
                        newRight.Enqueue((int)currentRight);

                        if (((Queue<object>)currentLeft).SequenceEqual(newRight))
                        {
                            continue;
                        }
                        else if (!IsInOrder((Queue<object>)currentLeft, newRight))
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
                        continue;
                    }
                }
            }

            return true;
        }
    }
}
