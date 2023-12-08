using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_8 : DayBase<long>
    {
        public day_8() : base("day_08.txt")
        {
            Console.WriteLine("Advent of Code - Day Eight");
        }

        public day_8(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            var inputQueue = new Queue<string>(_input);

            // parse instructions - first line
            var instructions = inputQueue.Dequeue();
            if (inputQueue.Peek() == string.Empty) inputQueue.Dequeue();

            // parse nodes of network
            Dictionary<string, (string left, string right)> network = new Dictionary<string, (string left, string right)>();
            while(inputQueue.TryDequeue(out string node))
            {
                string nodeId, leftNode, rightNode;
                RegexHelper.Match(node, @"(\w{3}) = \((\w{3}), (\w{3})\)", out nodeId, out leftNode, out rightNode);
                network.Add(nodeId, (leftNode, rightNode));
            }

            // Start at AAA, find ZZZ
            string currentNode = "AAA";
            int instructionPos = 0;

            while(currentNode != "ZZZ")
            {
                // get instruction
                char direction = instructions[instructionPos % instructions.Length];

                // get next node
                if(direction == 'L')
                {
                    currentNode = network[currentNode].left;
                }
                else if(direction == 'R')
                {
                    currentNode = network[currentNode].right;
                }

                instructionPos++;
            }

            return instructionPos;
        }

        public override long Problem2()
        {
            var inputQueue = new Queue<string>(_input);

            // parse instructions - first line
            var instructions = inputQueue.Dequeue();
            if (inputQueue.Peek() == string.Empty) inputQueue.Dequeue();

            // parse nodes of network
            Dictionary<string, (string left, string right)> network = new Dictionary<string, (string left, string right)>();
            while (inputQueue.TryDequeue(out string node))
            {
                string nodeId, leftNode, rightNode;
                RegexHelper.Match(node, @"(\w{3}) = \((\w{3}), (\w{3})\)", out nodeId, out leftNode, out rightNode);
                network.Add(nodeId, (leftNode, rightNode));
            }

            // Start at **A, find **Z
            List<string> currentNodes = network.Keys.Where(x => x.EndsWith('A')).ToList();
            List<long> instructionPos = Enumerable.Repeat((long)0, currentNodes.Count).ToList();

            while (!currentNodes.All(x => x.EndsWith('Z')))
            {
                for(int i = 0; i < currentNodes.Count; i++)
                {
                    // skip if we're already at **Z
                    if (currentNodes[i].EndsWith('Z')) continue;

                    // get instruction
                    char direction = instructions[(int)(instructionPos[i] % Convert.ToInt64(instructions.Length))];

                    // get next node
                    if (direction == 'L')
                    {
                        currentNodes[i] = network[currentNodes[i]].left;
                    }
                    else if (direction == 'R')
                    {
                        currentNodes[i] = network[currentNodes[i]].right;
                    }

                    instructionPos[i]++;
                }
            }

            return MathHelper.FindLowestCommonDenominator(instructionPos);
        }
    }
}