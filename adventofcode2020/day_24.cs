using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayTwentyFour
    {
        public static void Execute()
        {
            // bool: false (white), true (black)
            Dictionary<(int, int), bool> hexTiles = new Dictionary<(int, int), bool>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_24.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    (int, int) tileToFlip = FindHexTile(line);

                    if(hexTiles.ContainsKey(tileToFlip))
                    {
                        hexTiles[tileToFlip] = !hexTiles[tileToFlip];
                    }
                    else
                    {
                        hexTiles.Add(tileToFlip, true);
                    }
                }

                CountBlackTiles(hexTiles);
            }

            int iterations = 100;
            for(int i = 0; i < iterations; i++)
            {
                hexTiles = UpdateTiles(hexTiles);                
            }

            CountBlackTiles(hexTiles);
        }

        private static Dictionary<(int, int), bool> UpdateTiles(Dictionary<(int, int), bool> hexTiles)
        {
            Dictionary<(int, int), bool> newHexTiles = new Dictionary<(int, int), bool>(hexTiles);

            // find all white tiles in hexTiles
            var currentWhiteTiles = hexTiles.Where(x => x.Value == false).ToDictionary(x => x.Key, x => x.Value);

            // find all current black tiles, 
            var currentBlackTiles = hexTiles.Where(x => x.Value == true).ToDictionary(x => x.Key, x => x.Value);
            foreach(var tile in currentBlackTiles)
            {
                int blackNeighbors = FindNeighbors(hexTiles, tile.Key, currentWhiteTiles, true);
                if(blackNeighbors == 0 || blackNeighbors > 2)
                {
                    newHexTiles[tile.Key] = false;
                }
            }

            // find all white tile neighbors of black tiles that aren't tracked
            foreach(var tile in currentWhiteTiles)
            {
                int blackNeighbors = FindNeighbors(hexTiles, tile.Key, currentWhiteTiles, false);
                if(blackNeighbors == 2)
                {
                    newHexTiles[tile.Key] = true;
                }
            }


            return newHexTiles;
        }

        /// <summary>
        /// Adds missing white tiles to the white tile collection; returns number of neighboring black tiles for given tile. 
        /// </summary>
        /// <param name="hexTiles"></param>
        /// <param name="currentTile"></param>
        /// <param name="knownWhiteTiles"></param>
        /// <returns></returns>
        private static int FindNeighbors(Dictionary<(int, int), bool> hexTiles, (int, int) currentTile, Dictionary<(int, int), bool> knownWhiteTiles, bool addWhiteTilesFound)
        {
            // check tiles around current tile
            int blackCount = 0;

            bool foundNeighbor = hexTiles.TryGetValue((currentTile.Item1 + 1, currentTile.Item2), out bool neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1 + 1, currentTile.Item2), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1 + 1, currentTile.Item2), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            foundNeighbor = hexTiles.TryGetValue((currentTile.Item1, currentTile.Item2 + 1), out neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1, currentTile.Item2 + 1), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1, currentTile.Item2 + 1), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            foundNeighbor = hexTiles.TryGetValue((currentTile.Item1 - 1, currentTile.Item2 + 1), out neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1 - 1, currentTile.Item2 + 1), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1 - 1, currentTile.Item2 + 1), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            foundNeighbor = hexTiles.TryGetValue((currentTile.Item1 - 1, currentTile.Item2), out neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1 - 1, currentTile.Item2), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1 - 1, currentTile.Item2), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            foundNeighbor = hexTiles.TryGetValue((currentTile.Item1, currentTile.Item2 - 1), out neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1, currentTile.Item2 - 1), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1, currentTile.Item2 - 1), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            foundNeighbor = hexTiles.TryGetValue((currentTile.Item1 + 1, currentTile.Item2 - 1), out neighborColor);
            if (!foundNeighbor && !knownWhiteTiles.TryGetValue((currentTile.Item1 + 1, currentTile.Item2 - 1), out _) && addWhiteTilesFound)
            {
                knownWhiteTiles.Add((currentTile.Item1 + 1, currentTile.Item2 - 1), false);
            }
            else if(foundNeighbor && neighborColor)
            {
                blackCount++;
            }

            return blackCount;
        }

        private static void CountBlackTiles(Dictionary<(int, int), bool> hexTiles)
        {
            var countBlackTiles = hexTiles.Where(x => x.Value == true).Count();
            Console.WriteLine($"Number of black tiles: {countBlackTiles}");
        }
         
        private static (int, int) FindHexTile(string path)
        {
            (int, int) currentCoordinates = (0, 0);

            while (path.Length > 0)
            {
                // substring and find the next tile.                 
                string nextTile = "";
                if (path[0] == 'w' || path[0] == 'e')
                {
                    nextTile = path.Substring(0, 1);
                    path = path[1..];
                }
                else
                {
                    nextTile = path.Substring(0, 2);
                    path = path.Substring(2, path.Length - 2);
                }

                // coordinates change as follows (assuming starting from 0, 0): 
                // E: 1, 0
                // SE: 0, 1  
                // SW: -1, 1
                // W: -1, 0
                // NW: 0, -1
                // NE: 1, -1
                switch (nextTile)
                {
                    case "e":
                        currentCoordinates = (currentCoordinates.Item1 + 1, currentCoordinates.Item2);
                        break;
                    case "se":
                        currentCoordinates = (currentCoordinates.Item1, currentCoordinates.Item2 + 1); 
                        break;
                    case "sw":
                        currentCoordinates = (currentCoordinates.Item1 - 1, currentCoordinates.Item2 + 1);
                        break;
                    case "w":
                        currentCoordinates = (currentCoordinates.Item1 - 1, currentCoordinates.Item2);
                        break;
                    case "nw":
                        currentCoordinates = (currentCoordinates.Item1, currentCoordinates.Item2 - 1);
                        break;
                    case "ne":
                        currentCoordinates = (currentCoordinates.Item1 + 1, currentCoordinates.Item2 - 1);
                        break;
                }
            }

            return currentCoordinates;
        }
    }
}
