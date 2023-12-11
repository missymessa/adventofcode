using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

/// For future refactoring
/// Look at Pick's Theorem
/// 
/// The following code should be able to solve part 2 as long as the path has been walked.
/// Need to clear out "junk pipes"
/// int A2 = 0;
/// for (int i = 0; i < path.Count; i++)
/// {
///    A2 += path[i].x * path[(i + 1) % path.Count].y - path[i].y * path[(i + 1) % path.Count].x;
///}
///
///(A2 / 2 - path.Count / 2 + 1).Dump("Pick's Theorem + Shoelace Formula");

namespace adventofcode2023
{
    public class day_10 : DayBase<long>
    {
        private Dictionary<(int x, int y), List<(int x, int y)>> adjacencyList = new Dictionary<(int x, int y), List<(int x, int y)>>();
        private List<(int x, int y)> nodesInLoop = new List<(int x, int y)>();
        private int maxX = 0;
        private int maxY = 0;
        private (int x, int y) startingPosition = (0, 0);
        private string[] updatedInput;
        private long problem2Answer = 0;

        public day_10() : base("day_10.txt")
        {
            Console.WriteLine("Advent of Code - Day Ten");
            SetUp();
        }

        public day_10(string fileName) : base(fileName) 
        {
            SetUp();
        }

        public override long Problem1()
        {
            long problem1Answer = FindFarthestDistance(startingPosition, maxX, maxY);

            // remove any pipes that aren't a part of the loop
            foreach (var node in adjacencyList)
            {
                if (!nodesInLoop.Contains(node.Key))
                    adjacencyList.Remove(node.Key);
            }

            // count internal spaces            
            long enclosedSpaces = 0;

            int y = 0;

            foreach (var input in updatedInput)
            {
                char[] inputChar = input.ToCharArray();
                bool isOpen = true;

                for (int x = 0; x < inputChar.Length; x++)
                {
                    char c = inputChar[x];
                    List<(int x, int y)> value;
                    bool isInLoop = adjacencyList.TryGetValue((x, y), out value);

                    if (isInLoop && value.Count == 2 &&
                        (c == '|' ||
                        (c == 'F' && LookAhead(inputChar, x, 'J', '-')) ||
                        (c == 'L' && LookAhead(inputChar, x, '7', '-'))))
                        isOpen = !isOpen;

                    if ((c == '.' || (c != '.' && (!isInLoop || value.Count < 2))) && !isOpen)
                    {
                        enclosedSpaces++;
                        inputChar[x] = 'I';
                    }
                }

                y++;
            }

            problem2Answer = enclosedSpaces;

            return problem1Answer;
        }

        public override long Problem2()
        {
            if(problem2Answer == 0) Problem1();

            return problem2Answer;            
        }

        private bool LookAhead(char[] line, int startingIndex, char ending, char ignore)
        {
            int index = startingIndex + 1;

            while(index < line.Length)
            {
                if(line[index] == ending)
                    return true;

                if (line[index] != ignore)
                    return false;

                index++;
            }

            return false;
        }

        private void SetUp()
        {
            maxX = _input[0].Length;
            maxY = _input.Count;
            updatedInput = new string[maxY];

            int y = 0;

            foreach (var input in _input)
            {
                char[] inputChar = input.ToCharArray();

                for (int x = 0; x < inputChar.Length; x++)
                {
                    if (inputChar[x] != '.')
                    {
                        adjacencyList[(x, y)] = new List<(int x, int y)>();
                    }

                    switch (inputChar[x])
                    {
                        case '|':
                            if (y > 0 && (_input[y - 1][x] != 'L' && _input[y - 1][x] != 'J' && _input[y - 1][x] != '-')) AddEdge((x, y), (x, y - 1));
                            if (y < maxY - 1 && (_input[y + 1][x] != 'F' && _input[y + 1][x] != '7' && _input[y + 1][x] != '-')) AddEdge((x, y), (x, y + 1));
                            break;
                        case '-':
                            if (x > 0 && (_input[y][x - 1] != '7' && _input[y][x - 1] != 'J' && _input[y][x - 1] != '|')) AddEdge((x, y), (x - 1, y));
                            if (x < maxX - 1 && (_input[y][x + 1] != 'L' && _input[y][x + 1] != 'F' && _input[y][x + 1] != '|')) AddEdge((x, y), (x + 1, y));
                            break;
                        case 'L':
                            if (y > 0 && (_input[y - 1][x] != 'L' && _input[y - 1][x] != 'J' && _input[y - 1][x] != '-')) AddEdge((x, y), (x, y - 1));
                            if (x < maxX - 1 && (_input[y][x + 1] != 'L' && _input[y][x + 1] != 'F' && _input[y][x + 1] != '|')) AddEdge((x, y), (x + 1, y));
                            break;
                        case 'J':
                            if (y > 0 && (_input[y - 1][x] != 'L' && _input[y - 1][x] != 'J' && _input[y - 1][x] != '-')) AddEdge((x, y), (x, y - 1));
                            if (x > 0 && (_input[y][x - 1] != '7' && _input[y][x - 1] != 'J' && _input[y][x - 1] != '|')) AddEdge((x, y), (x - 1, y));
                            break;
                        case '7':
                            if (y < maxY - 1 && (_input[y + 1][x] != 'F' && _input[y + 1][x] != '7' && _input[y + 1][x] != '-')) AddEdge((x, y), (x, y + 1));
                            if (x > 0 && (_input[y][x - 1] != '7' && _input[y][x - 1] != 'J' && _input[y][x - 1] != '|')) AddEdge((x, y), (x - 1, y));
                            break;
                        case 'F':
                            if (y < maxY - 1 && (_input[y + 1][x] != 'F' && _input[y + 1][x] != '7' && _input[y + 1][x] != '-')) AddEdge((x, y), (x, y + 1));
                            if (x < maxX - 1 && (_input[y][x + 1] != 'L' && _input[y][x + 1] != 'F' && _input[y][x + 1] != '|')) AddEdge((x, y), (x + 1, y));
                            break;
                        case 'S':
                            startingPosition = (x, y);
                            List<char> possiblePipes = new List<char>();
                            // based on what tiles are around it, determine the shape of the pipe at S
                            if (y > 0 && (_input[y - 1][x] == '|' || _input[y - 1][x] == '7' || _input[y - 1][x] == 'F'))
                            {
                                AddEdge((x, y), (x, y - 1));
                                possiblePipes.AddRange(new[] { '|', 'J', 'L' });
                            }

                            if (y < maxY - 1 && (_input[y + 1][x] == '|' || _input[y + 1][x] == 'J' || _input[y + 1][x] == 'L'))
                            {
                                AddEdge((x, y), (x, y + 1));
                                possiblePipes.AddRange(new[] { '|', 'F', '7' });
                            }

                            if (x > 0 && (_input[y][x - 1] == '-' || _input[y][x - 1] == 'F' || _input[y][x - 1] == 'L'))
                            {
                                AddEdge((x, y), (x - 1, y));
                                possiblePipes.AddRange(new[] { '-', '7', 'J' });
                            }

                            if (x < maxX - 1 && (_input[y][x + 1] == '-' || _input[y][x + 1] == '7' || _input[y][x + 1] == 'J'))
                            {
                                AddEdge((x, y), (x + 1, y));
                                possiblePipes.AddRange(new[] { '-', 'F', 'L' });
                            }

                            var newS = possiblePipes.GroupBy(c => c)
                                     .Where(group => group.Count() > 1)
                                     .Select(group => group.Key)
                                     .FirstOrDefault();

                            inputChar[x] = newS;

                            break;
                    }
                }

                updatedInput[y] = new string(inputChar);

                y++;
            }
        }

        public void AddEdge((int x, int y) v, (int x, int y) w)
        {
            adjacencyList[v].Add(w);
        }

        public long FindFarthestDistance((int x, int y) startVertex, int maxX, int maxY)
        {
            bool[,] visited = new bool[maxX, maxY]; // Assuming maximum x and y values

            // Initialize a queue for BFS
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            queue.Enqueue(startVertex);
            visited[startVertex.Item1, startVertex.Item2] = true;
            
            nodesInLoop.Add(startVertex);

            // Initialize distance to 0
            long distance = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                // Process all vertices at the current level
                for (int i = 0; i < levelSize; ++i)
                {
                    (int x, int y) vertex = queue.Dequeue();

                    foreach (var neighbor in adjacencyList[vertex])
                    {
                        if (!visited[neighbor.Item1, neighbor.Item2])
                        {
                            visited[neighbor.Item1, neighbor.Item2] = true;
                            if(!nodesInLoop.Contains(neighbor)) nodesInLoop.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                // Increment distance after processing each level
                distance++;
            }

            // Subtract 1 to get the farthest distance from the starting vertex
            return distance - 1;
        }
    }
}