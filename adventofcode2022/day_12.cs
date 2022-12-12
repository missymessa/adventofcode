using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayTwelve
    {
        private static char[][] map;
        private static int mapRows;
        private static int mapCols;

        public static void Execute()
        {
            List<string> mapInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_12.txt")).ToList();

            (int x, int y) startingLoc = (0, 0);
            (int x, int y) endingLoc = (0, 0);
            List<(int x, int y)> lowestLoc = new List<(int x, int y)>();

            map = new char[mapInput.Count][];

            for(int i = 0; i < mapInput.Count; i++)
            {
                map[i] = mapInput[i].ToCharArray();

                for(int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'S')
                    {
                        startingLoc = (i, j);
                        map[i][j] = 'a';
                    }
                    else if (map[i][j] == 'E')
                    {
                        endingLoc = (i, j);
                        map[i][j] = 'z';
                    }
                    else if(map[i][j] == 'a')
                    {
                        lowestLoc.Add((i, j));
                    }
                }
            }

            mapRows = map.Length;
            mapCols = map[0].Length;

            int fewestSteps = MoveCountBFS(startingLoc, endingLoc);

            Console.WriteLine("Problem 1: {0}", fewestSteps);

            foreach (var loc in lowestLoc)
            {
                int currentSteps = MoveCountBFS(loc, endingLoc);

                if (currentSteps < fewestSteps)
                    fewestSteps = currentSteps;
            }

            Console.WriteLine("Problem 2: {0}", fewestSteps);
        }

        private static int MoveCountBFS((int x, int y) startingLoc, (int x, int y) endingLoc)
        {
            Queue<(int x, int y)> bfsTraverse = new Queue<(int x, int y)>();
            bool[,] visited = new bool[mapRows, mapCols];
            (int x, int y) currentLoc = startingLoc;
            int nodesInCurrentLayer = 1;
            int nodesInNextLayer = 0;
            int moveCount = 0;

            int[] dRow = { -1, 0, 1, 0 };
            int[] dCol = { 0, 1, 0, -1 };

            bfsTraverse.Enqueue(currentLoc);
            visited[currentLoc.x, currentLoc.y] = true;
            bool pathFound = false;

            while (bfsTraverse.Count > 0)
            {
                currentLoc = bfsTraverse.Dequeue();
                if (currentLoc == endingLoc)
                {
                    pathFound = true;
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    (int x, int y) nextNode = (currentLoc.x + dRow[i], currentLoc.y + dCol[i]);
                    if (isValid(currentLoc, nextNode))
                    {
                        bfsTraverse.Enqueue(nextNode);
                        visited[nextNode.x, nextNode.y] = true;
                        nodesInNextLayer++;
                    }
                }

                nodesInCurrentLayer--;

                if (nodesInCurrentLayer == 0)
                {
                    nodesInCurrentLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    moveCount++;
                }
            }

            return pathFound ? moveCount : int.MaxValue;

            bool isValid((int x, int y) currentNode, (int x, int y) nextNode)
            {
                // in bounds? 
                if (nextNode.x < 0 || nextNode.y < 0 || nextNode.x >= mapRows || nextNode.y >= mapCols)
                    return false;

                // have not already visited? 
                if (visited[nextNode.x, nextNode.y])
                    return false;

                // correct height?
                int heightDiff = map[nextNode.x][nextNode.y] - map[currentNode.x][currentNode.y];
                if (heightDiff > 1)
                    return false;

                return true;
            }
        }
    }
}
