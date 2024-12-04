using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;

namespace adventofcode2024
{
    public class day_4 : DayBase<int>
    {
        public day_4() : base("day_04.txt")
        {
            Console.WriteLine("Advent of Code - Day Four");
        }

        public day_4(string fileName) : base(fileName) { }

        private static string[][] wordSearch;

        public override int Problem1()
        {
            int xmasCount = 0; 

            string[] wordSearchInput = _input.ToArray();
            wordSearch = new string[wordSearchInput.Length][];

            for (int i = 0; i < wordSearchInput.Length; i++)
            {
                wordSearch[i] = new string[wordSearchInput[i].Length];
                for (int j = 0; j < wordSearchInput[i].Length; j++)
                {
                    wordSearch[i][j] = wordSearchInput[i].ToCharArray()[j].ToString();
                }
            }

            // iterate through the word search
            // for each X encountered, check each direction for a XMAS
            for (int y = 0; y < wordSearch.Length; y++)
            {
                for(int x = 0; x < wordSearch[y].Length; x++)
                {
                    if(wordSearch[y][x] == "X")
                    {
                        xmasCount += SearchXMAS(x, y);
                    }
                }
            }            

            return xmasCount;
        }

        private int SearchXMAS(int x, int y)
        {
            int xmasCount = 0;

            // check up
            if (y - 1 >= 0 && wordSearch[y - 1][x] == "M")
            {
                // check up
                if (y - 2 >= 0 && wordSearch[y - 2][x] == "A")
                {
                    // check up
                    if (y - 3 >= 0 && wordSearch[y - 3][x] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check down
            if (y + 1 < wordSearch.Length && wordSearch[y + 1][x] == "M")
            {
                // check down
                if (y + 2 < wordSearch.Length && wordSearch[y + 2][x] == "A")
                {
                    // check down
                    if (y + 3 < wordSearch.Length && wordSearch[y + 3][x] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check left
            if (x - 1 >= 0 && wordSearch[y][x - 1] == "M")
            {
                // check left
                if (x - 2 >= 0 && wordSearch[y][x - 2] == "A")
                {
                    // check left
                    if (x - 3 >= 0 && wordSearch[y][x - 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check right
            if (x + 1 < wordSearch[y].Length && wordSearch[y][x + 1] == "M")
            {
                // check right
                if (x + 2 < wordSearch[y].Length && wordSearch[y][x + 2] == "A")
                {
                    if (x + 3 < wordSearch[y].Length && wordSearch[y][x + 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check NW
            if(y - 1 >= 0 && x - 1 >= 0 && wordSearch[y - 1][x - 1] == "M")
            {
                if (y - 2 >= 0 && x - 2 >= 0 && wordSearch[y - 2][x - 2] == "A")
                {
                    if (y - 3 >= 0 && x - 3 >= 0 && wordSearch[y - 3][x - 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check NE
            if (y - 1 >= 0 && x + 1 < wordSearch[y].Length && wordSearch[y - 1][x + 1] == "M")
            {
                if (y - 2 >= 0 && x + 2 < wordSearch[y].Length && wordSearch[y - 2][x + 2] == "A")
                {
                    if (y - 3 >= 0 && x + 3 < wordSearch[y].Length && wordSearch[y - 3][x + 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check SW
            if (y + 1 < wordSearch.Length && x - 1 >= 0 && wordSearch[y + 1][x - 1] == "M")
            {
                if (y + 2 < wordSearch.Length && x - 2 >= 0 && wordSearch[y + 2][x - 2] == "A")
                {
                    if (y + 3 < wordSearch.Length && x - 3 >= 0 && wordSearch[y + 3][x - 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            // check SE
            if (y + 1 < wordSearch.Length && x + 1 < wordSearch[y].Length && wordSearch[y + 1][x + 1] == "M")
            {
                if (y + 2 < wordSearch.Length && x + 2 < wordSearch[y].Length && wordSearch[y + 2][x + 2] == "A")
                {
                    if (y + 3 < wordSearch.Length && x + 3 < wordSearch[y].Length && wordSearch[y + 3][x + 3] == "S")
                    {
                        xmasCount++;
                    }
                }
            }

            return xmasCount;
        }

        private int SearchX_MAS(int x, int y)
        {
            int x_masCount = 0;

            if (y - 1 >= 0 && y + 1 < wordSearch.Length && x - 1 >= 0 && x + 1 < wordSearch[y].Length)
            {
                // check upside down
                if (wordSearch[y + 1][x - 1] == "M" && wordSearch[y + 1][x + 1] == "M" &&
                    wordSearch[y - 1][x - 1] == "S" && wordSearch[y - 1][x + 1] == "S")
                {
                    x_masCount++;
                }

                // check right-side up
                if (wordSearch[y - 1][x - 1] == "M" && wordSearch[y - 1][x + 1] == "M" &&
                    wordSearch[y + 1][x - 1] == "S" && wordSearch[y + 1][x + 1] == "S")
                {
                    x_masCount++;
                }

                // check rotated left
                if (wordSearch[y - 1][x - 1] == "M" && wordSearch[y + 1][x - 1] == "M" &&
                    wordSearch[y + 1][x + 1] == "S" && wordSearch[y - 1][x + 1] == "S")
                {
                    x_masCount++;
                }

                // check rotated right
                if (wordSearch[y - 1][x - 1] == "S" && wordSearch[y + 1][x - 1] == "S" &&
                    wordSearch[y + 1][x + 1] == "M" && wordSearch[y - 1][x + 1] == "M")
                {
                    x_masCount++;
                }
            }

            return x_masCount;
        }

        public override int Problem2()
        {
            int xmasCount = 0;

            string[] wordSearchInput = _input.ToArray();
            wordSearch = new string[wordSearchInput.Length][];

            for (int i = 0; i < wordSearchInput.Length; i++)
            {
                wordSearch[i] = new string[wordSearchInput[i].Length];
                for (int j = 0; j < wordSearchInput[i].Length; j++)
                {
                    wordSearch[i][j] = wordSearchInput[i].ToCharArray()[j].ToString();
                }
            }

            // iterate through the word search
            // for each X encountered, check each direction for a XMAS
            for (int y = 0; y < wordSearch.Length; y++)
            {
                for (int x = 0; x < wordSearch[y].Length; x++)
                {
                    if (wordSearch[y][x] == "A")
                    {
                        xmasCount += SearchX_MAS(x, y);
                    }
                }
            }

            return xmasCount;
        }
    }
}
