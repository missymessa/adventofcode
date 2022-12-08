using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayEight
    {
        private static int[][] trees;

        public static void Execute()
        {
            string[] treeInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_08.txt")).ToArray();
            trees = new int[treeInput.Length][];
            
            for(int i = 0; i < treeInput.Length; i++)
            {
                trees[i] = new int[treeInput[i].Length];
                for(int j = 0; j < treeInput[i].Length; j++)
                {
                    trees[i][j] = treeInput[i].ToCharArray()[j].GetNumericValueInteger();
                }
            }

            // Problem 1:

            int visibleTreeCount = 0;

            for (int i = 0; i < treeInput.Length; i++)
            {
                for (int j = 0; j < treeInput[i].Length; j++)
                {
                    if(TreeIsVisible(i, j))
                    {
                        visibleTreeCount++;
                    }
                }
            }

            Console.WriteLine("Problem 1: {0}", visibleTreeCount);

            // Problem 2: 

            int bestScenicScore = 0;

            for (int i = 0; i < treeInput.Length; i++)
            {
                for (int j = 0; j < treeInput[i].Length; j++)
                {
                    int scenicScore = TreeScenicScore(i, j);
                    if (scenicScore > bestScenicScore)
                    {
                        bestScenicScore = scenicScore;
                    }
                }
            }

            Console.WriteLine("Problem 2: {0}", bestScenicScore);
        }

        private static bool TreeIsVisible(int x, int y)
        {
            int treeHeight = trees[x][y];
            List<bool> visible = new List<bool>();
            bool currentVisible = true;

            if (x == 0 || y == 0 || x == trees.Length - 1 || y == trees[x].Length - 1)
                return true;

            // check column
            for (int i = 0; i < x; i++)
            {
                if (trees[i][y] >= treeHeight)
                {
                    currentVisible = false;
                }
            }
            visible.Add(currentVisible);

            currentVisible = true;
            for (int i = x + 1; i < trees.Length; i++)
            {
                if (trees[i][y] >= treeHeight)
                {
                    currentVisible = false;
                }
            }
            visible.Add(currentVisible);

            // check row
            currentVisible = true;
            for (int i = 0; i < y; i++)
            {
                if (trees[x][i] >= treeHeight)
                {
                    currentVisible = false;
                }
            }
            visible.Add(currentVisible);

            currentVisible = true;
            for (int i = y + 1; i < trees[x].Length; i++)
            {
                if (trees[x][i] >= treeHeight)
                {
                    currentVisible = false;
                }
            }
            visible.Add(currentVisible);

            return visible.Any(x => x == true);
        }

        private static int TreeScenicScore(int x, int y)
        {
            int treeHeight = trees[x][y];
            List<int> numTreesVisible = new List<int>();
            int currentNumTreesVisible = 0;

            // check column
            for (int i = x - 1; i >= 0; i--)
            {
                if (trees[i][y] < treeHeight)
                {
                    currentNumTreesVisible++;
                }
                else if (trees[i][y] >= treeHeight)
                {
                    currentNumTreesVisible++;
                    break;
                }
            }
            numTreesVisible.Add(currentNumTreesVisible);

            currentNumTreesVisible = 0;
            for (int i = x + 1; i < trees.Length; i++)
            {
                if (trees[i][y] < treeHeight)
                {
                    currentNumTreesVisible++;
                }
                else if (trees[i][y] >= treeHeight)
                {
                    currentNumTreesVisible++;
                    break;
                }
            }
            numTreesVisible.Add(currentNumTreesVisible);

            // check row
            currentNumTreesVisible = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                if (trees[x][i] < treeHeight)
                {
                    currentNumTreesVisible++;
                }
                else if (trees[x][i] >= treeHeight)
                {
                    currentNumTreesVisible++;
                    break;
                }
            }
            numTreesVisible.Add(currentNumTreesVisible);

            currentNumTreesVisible = 0;
            for (int i = y + 1; i < trees[x].Length; i++)
            {
                if (trees[x][i] < treeHeight)
                {
                    currentNumTreesVisible++;
                }
                else if (trees[x][i] >= treeHeight)
                {
                    currentNumTreesVisible++;
                    break;
                }
            }
            numTreesVisible.Add(currentNumTreesVisible);

            return numTreesVisible.Aggregate((x, y) => x * y);
        }
    }
}
