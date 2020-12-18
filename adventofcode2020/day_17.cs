using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DaySeventeen
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        public static void ProblemOne()
        {
            Dictionary<int, List<string>> cubes = new Dictionary<int, List<string>>();
            cubes.Add(0, CreateEmptyYPlane(10, 10));
            cubes.Add(1, File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_17.txt")).ToList());
            cubes.Add(2, CreateEmptyYPlane(10, 10));

            int cycles = 6;

            for(int i = 0; i < cycles; i++)
            {
                cubes = CycleCubes(cubes);
            }

            Console.WriteLine($"Number of active cubes: {CountActiveCubes(cubes)}");
        }

        public static void ProblemTwo()
        {
            Dictionary<int, List<string>> cubes = new Dictionary<int, List<string>>();
            cubes.Add(0, CreateEmptyYPlane(10, 10));
            cubes.Add(1, File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_17.txt")).ToList());
            cubes.Add(2, CreateEmptyYPlane(10, 10));

            Dictionary<int, Dictionary<int, List<string>>> hypercubes = new Dictionary<int, Dictionary<int, List<string>>>();
            hypercubes.Add(0, CreateEmptyZPlane(10, 10, 3));
            hypercubes.Add(1, cubes);
            hypercubes.Add(2, CreateEmptyZPlane(10, 10, 3));

            int cycles = 6;

            for (int i = 0; i < cycles; i++)
            {
                hypercubes = CycleHyperCubes(hypercubes);
            }

            Console.WriteLine($"Number of active hyper cubes: {CountActiveHyperCubes(hypercubes)}");
        }

        private static Dictionary<int, Dictionary<int, List<string>>> CycleHyperCubes(Dictionary<int, Dictionary<int, List<string>>> hypercubes)
        {
            int newXLength = 0;
            int newYLength = 0;
            int newZLength = 0;
            int newWLength = hypercubes.Count + 2;

            Dictionary<int, Dictionary<int, List<string>>> newHyperCubes = new Dictionary<int, Dictionary<int, List<string>>>();

            for(int w = 0; w < hypercubes.Count; w++)
            {
                var zElement = hypercubes.ElementAt(w).Value;
                Dictionary<int, List<string>> newZ = new Dictionary<int, List<string>>();
                newZLength = zElement.Count + 2;

                for (int z = 0; z < zElement.Count; z++)
                {
                    var yElement = zElement.ElementAt(z).Value;
                    List<string> newY = new List<string>();
                    newYLength = yElement.Count + 2;                    

                    for (int y = 0; y < yElement.Count; y++)
                    {
                        var xElement = yElement.ElementAt(y);
                        string newX = ".";
                        newXLength = xElement.Length + 2;

                        if (y == 0)
                        {
                            newY.Add(CreateEmptyXPlane(newXLength));
                        }

                        for (int x = 0; x < xElement.Length; x++)
                        {
                            int activeNeighbors = CountActiveHyperNeighbors(x, y, z, w, xElement.Length - 1, yElement.Count - 1, zElement.Count - 1, hypercubes.Count -1, hypercubes);
                            // if active, check to see if 2 or 3 neighbors are active, if not, go inactive
                            if (xElement[x] == '#')
                            {
                                if (activeNeighbors == 2 || activeNeighbors == 3)
                                {
                                    newX += '#';
                                }
                                else
                                {
                                    newX += '.';
                                }
                            }
                            // if inactive, check to see if 3 neighbors are active, if so, go active
                            else if (xElement[x] == '.')
                            {
                                if (activeNeighbors == 3)
                                {
                                    newX += '#';
                                }
                                else
                                {
                                    newX += '.';
                                }
                            }
                        }
                        newX += ".";
                        newY.Add(newX);
                    }
                    newY.Add(CreateEmptyXPlane(newXLength));

                    if (z == 0)
                    {
                        newZ.Add(0, CreateEmptyYPlane(newXLength, newYLength));
                    }
                    newZ.Add(z + 1, newY);
                }
                newZ.Add(newZLength - 1, CreateEmptyYPlane(newXLength, newYLength));

                if (w == 0)
                {
                    newHyperCubes.Add(0, CreateEmptyZPlane(newXLength, newYLength, newZLength));
                }
                newHyperCubes.Add(w + 1, newZ);
            }

            newHyperCubes.Add(newWLength - 1, CreateEmptyZPlane(newXLength, newYLength, newZLength));
            return newHyperCubes;
        }

        private static Dictionary<int, List<string>> CycleCubes(Dictionary<int, List<string>> cubes)
        {
            int newXLength = 0;
            int newYLength = 0;
            int newZLength = cubes.Count + 2;

            Dictionary<int, List<string>> newCubes = new Dictionary<int, List<string>>();            

            for(int z = 0; z < cubes.Count; z++)
            {
                var yElement = cubes.ElementAt(z).Value;
                List<string> newY = new List<string>();
                newYLength = yElement.Count + 2;

                for (int y = 0; y < yElement.Count; y++)
                {
                    var xElement = yElement.ElementAt(y);                    
                    string newX = ".";
                    newXLength = xElement.Length + 2;

                    if(y == 0)
                    {
                        newY.Add(CreateEmptyXPlane(newXLength));
                    }

                    for (int x = 0; x < xElement.Length; x++)
                    {                        
                        int activeNeighbors = CountActiveNeighbors(x, y, z, xElement.Length - 1, yElement.Count - 1, cubes.Count - 1, cubes);
                        // if active, check to see if 2 or 3 neighbors are active, if not, go inactive
                        if (xElement[x] == '#')
                        {
                            if(activeNeighbors == 2 || activeNeighbors == 3)
                            {
                                newX += '#';
                            }
                            else
                            {
                                newX += '.';
                            }
                        }
                        // if inactive, check to see if 3 neighbors are active, if so, go active
                        else if(xElement[x] == '.')
                        {
                            if(activeNeighbors == 3)
                            {
                                newX += '#';
                            }
                            else
                            {
                                newX += '.';
                            }
                        }
                    }
                    newX += ".";
                    newY.Add(newX);
                }
                newY.Add(CreateEmptyXPlane(newXLength));

                if(z == 0)
                {
                    newCubes.Add(0, CreateEmptyYPlane(newXLength, newYLength));
                }
                newCubes.Add(z + 1, newY);
            }

            newCubes.Add(newZLength - 1, CreateEmptyYPlane(newXLength, newYLength));
            return newCubes;
        }

        private static int CountActiveNeighbors(int x, int y, int z, int maxX, int maxY, int maxZ, Dictionary<int, List<string>> baseCubes)
        {
            int activeNeighbors = 0;

            // calculate max and mins of all coordinates and check validity
            int calculatedMaxX = (x + 1) > maxX ? maxX : (x + 1);
            int calculatedMinX = (x - 1) < 0 ? 0 : (x - 1);

            int calculatedMaxY = (y + 1) > maxY ? maxY : (y + 1);
            int calculatedMinY = (y - 1) < 0 ? 0 : (y - 1);

            int calculatedMaxZ = (z + 1) > maxZ ? maxZ : (z + 1);
            int calculatedMinZ = (z - 1) < 0 ? 0 : (z - 1);

            // check all neighbors and return value
            for (int zPos = calculatedMinZ; zPos <= calculatedMaxZ; zPos++)
            {
                var yElement = baseCubes.ElementAt(zPos).Value;
                for (int yPos = calculatedMinY; yPos <= calculatedMaxY; yPos++)
                {
                    var xElement = yElement.ElementAt(yPos);
                    for (int xPos = calculatedMinX; xPos <= calculatedMaxX; xPos++)
                    {
                        if (xElement[xPos] == '#' && !(xPos == x && yPos == y && zPos == z)) activeNeighbors++;
                    }
                }
            }

            return activeNeighbors;
        }

        private static int CountActiveHyperNeighbors(int x, int y, int z, int w, int maxX, int maxY, int maxZ, int maxW, Dictionary<int, Dictionary<int, List<string>>> baseHyperCubes)
        {
            int activeNeighbors = 0;

            // calculate max and mins of all coordinates and check validity
            int calculatedMaxX = (x + 1) > maxX ? maxX : (x + 1);
            int calculatedMinX = (x - 1) < 0 ? 0 : (x - 1);

            int calculatedMaxY = (y + 1) > maxY ? maxY : (y + 1);
            int calculatedMinY = (y - 1) < 0 ? 0 : (y - 1);

            int calculatedMaxZ = (z + 1) > maxZ ? maxZ : (z + 1);
            int calculatedMinZ = (z - 1) < 0 ? 0 : (z - 1);

            int calculatedMaxW = (w + 1) > maxW ? maxW : (w + 1);
            int calculatedMinW = (w - 1) < 0 ? 0 : (w - 1);

            // check all neighbors and return value
            for (int wPos = calculatedMinW; wPos <= calculatedMaxW; wPos++)
            {
                var zElement = baseHyperCubes.ElementAt(wPos).Value;
                for (int zPos = calculatedMinZ; zPos <= calculatedMaxZ; zPos++)
                {
                    var yElement = zElement.ElementAt(zPos).Value;
                    for (int yPos = calculatedMinY; yPos <= calculatedMaxY; yPos++)
                    {
                        var xElement = yElement.ElementAt(yPos);
                        for (int xPos = calculatedMinX; xPos <= calculatedMaxX; xPos++)
                        {
                            if (xElement[xPos] == '#' && !(xPos == x && yPos == y && zPos == z && wPos == w)) activeNeighbors++;
                        }
                    }
                }
            }

            return activeNeighbors;
        }

        private static int CountActiveCubes(Dictionary<int, List<string>> cubes)
        {
            int activeCubes = 0;

            foreach(var z in cubes)
            {
                foreach(var y in z.Value)
                {
                    activeCubes += y.Count(x => x.Equals('#'));
                }
            }

            return activeCubes;
        }

        private static int CountActiveHyperCubes(Dictionary<int, Dictionary<int, List<string>>> hypercubes)
        {
            int activeCubes = 0;

            foreach (var w in hypercubes)
            {
                foreach (var z in w.Value)
                {
                    foreach (var y in z.Value)
                    {
                        activeCubes += y.Count(x => x.Equals('#'));
                    }
                }
            }

            return activeCubes;
        }

        private static Dictionary<int, List<string>> CreateEmptyZPlane(int xLength, int yLength, int zLength)
        {
            List<string> emptyY = CreateEmptyYPlane(xLength, yLength);

            Dictionary<int, List<string>> emptyZ = new Dictionary<int, List<string>>();
            for(int k = 0; k < zLength; k++)
            {
                emptyZ.Add(k, emptyY);
            }

            return emptyZ;
        }

        private static List<string> CreateEmptyYPlane(int xLength, int yLength)
        {
            string emptyX = CreateEmptyXPlane(xLength);

            List<string> emptyY = new List<string>();
            for(int j = 0; j < yLength; j++)
            {
                emptyY.Add(emptyX);
            }

            return emptyY;
        }

        private static string CreateEmptyXPlane(int xLength)
        {
            string emptyX = "";
            for (int i = 0; i < xLength; i++)
            {
                emptyX += '.';
            }

            return emptyX;
        }
    }
}
