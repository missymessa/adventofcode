using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_3 : DayBase<int>
    {
        public day_3() : base("day_03.txt")
        {
            Console.WriteLine("Advent of Code - Day Three");
        }

        public day_3(string fileName) : base(fileName) { }

        private static string[][] schematic;

        public override int Problem1()
        {
            int partTotal = 0;

            // load file input into 2D array
            string[] schematicInput = _input.ToArray();
            schematic = new string[schematicInput.Length][];

            for (int i = 0; i < schematicInput.Length; i++)
            {
                schematic[i] = new string[schematicInput[i].Length];
                for (int j = 0; j < schematicInput[i].Length; j++)
                {
                    schematic[i][j] = schematicInput[i].ToCharArray()[j].ToString();
                }
            }

            // find numbers, check to see if there are any symbols around them, if so, add to total
            int fullPartNumber = 0;

            for (int i = 0; i < schematicInput.Length; i++)
            {
                for (int j = 0; j < schematicInput[i].Length; j++)
                {
                    // get full part number
                    string currentPartNumber = "";
                    int lastXPos = 0;
                    bool isPart = false;


                    if (Regex.IsMatch(schematic[i][j], @"\d"))
                    {
                        // find full number front to back
                        int pointer = j;
                        while (Regex.IsMatch(schematic[i][pointer], @"\d") && pointer > 0 && Regex.IsMatch(schematic[i][pointer - 1], @"\d"))
                        {
                            pointer--;
                        }

                        while (pointer >= 0 && pointer < schematic[i].Length)
                        {
                            if (Regex.IsMatch(schematic[i][pointer], @"\d"))
                            {
                                currentPartNumber += schematic[i][pointer];
                                if (!isPart)
                                {
                                    isPart = IsSymbolNearby(pointer, i);
                                }
                            }
                            else
                            {
                                break;
                            }
                            pointer++;
                        }

                        lastXPos = pointer;

                        fullPartNumber = Convert.ToInt32(currentPartNumber);
                    }
                    else
                    {
                        continue;
                    }

                    if (isPart)
                    {
                        partTotal += fullPartNumber;
                    }

                    j = lastXPos;
                }
            }

            return partTotal;
        }

        public override int Problem2()
        {
            // load file input into 2D array
            string[] schematicInput = _input.ToArray();
            schematic = new string[schematicInput.Length][];

            for (int i = 0; i < schematicInput.Length; i++)
            {
                schematic[i] = new string[schematicInput[i].Length];
                for (int j = 0; j < schematicInput[i].Length; j++)
                {
                    schematic[i][j] = schematicInput[i].ToCharArray()[j].ToString();
                }
            }

            // find numbers, check to see if there are any symbols around them, if so, add to total
            int fullPartNumber = 0;
            List<Gear> gears = new List<Gear>();

            for (int i = 0; i < schematicInput.Length; i++)
            {
                for (int j = 0; j < schematicInput[i].Length; j++)
                {
                    // get full part number
                    string currentPartNumber = "";
                    int lastXPos = 0;
                    bool isGearPart = false;
                    int gearXPos = 0;
                    int gearYPos = 0;

                    if (Regex.IsMatch(schematic[i][j], @"\d"))
                    {
                        // find full number front to back
                        int pointer = j;
                        while (Regex.IsMatch(schematic[i][pointer], @"\d") && pointer > 0 && Regex.IsMatch(schematic[i][pointer - 1], @"\d"))
                        {
                            pointer--;
                        }

                        while (pointer >= 0 && pointer < schematic[i].Length)
                        {
                            if (Regex.IsMatch(schematic[i][pointer], @"\d"))
                            {
                                currentPartNumber += schematic[i][pointer];
                                if (!isGearPart)
                                {
                                    isGearPart = IsGearSymbolNearby(pointer, i, out gearXPos, out gearYPos);
                                }
                            }
                            else
                            {
                                break;
                            }
                            pointer++;
                        }

                        lastXPos = pointer;

                        fullPartNumber = Convert.ToInt32(currentPartNumber);
                    }
                    else
                    {
                        continue;
                    }

                    if (isGearPart)
                    {
                        gears.Add(new Gear(fullPartNumber, gearXPos, gearYPos));
                    }

                    j = lastXPos;
                }
            }

            int ratioSum = 0;

            // iterate through gears and match up parts with the corresponding gear position and find the ratio. 
            while(gears.Count > 0)
            {
                Gear gear = gears[0];
                gears.Remove(gear);

                Gear secondGear = gears.Find(g => g.GearXPos == gear.GearXPos && g.GearYPos == gear.GearYPos);
                if (secondGear != null)
                {
                    gears.Remove(secondGear);

                    int ratio = gear.PartId * secondGear.PartId;
                    ratioSum += ratio;
                }
            }

            // return the sum of all the ratios. 
            return ratioSum;
        }

        private bool IsSymbolNearby(int x, int y)
        {
            if ((x > 0 && y > 0 && Regex.IsMatch(schematic[y - 1][x - 1], @"[^\d.]")) ||
               (y > 0 && Regex.IsMatch(schematic[y - 1][x], @"[^\d.]")) ||
               (y > 0 && x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y - 1][x + 1], @"[^\d.]")) ||
               (x > 0 && Regex.IsMatch(schematic[y][x - 1], @"[^\d.]")) ||
               (x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y][x + 1], @"[^\d.]")) ||
               (y < schematic.Length - 1 && x > 0 && Regex.IsMatch(schematic[y + 1][x - 1], @"[^\d.]")) ||
               (y < schematic.Length - 1 && Regex.IsMatch(schematic[y + 1][x], @"[^\d.]")) ||
               (y < schematic.Length - 1 && x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y + 1][x + 1], @"[^\d.]")))
            {
                return true;
            }

            return false;
        }

        private bool IsGearSymbolNearby(int x, int y, out int gearXPos, out int gearYPos)
        {
            if (x > 0 && y > 0 && Regex.IsMatch(schematic[y - 1][x - 1], @"(\*)"))
            {
                gearXPos = x - 1;
                gearYPos = y - 1;
                return true;
            }
            else if(y > 0 && Regex.IsMatch(schematic[y - 1][x], @"(\*)"))
            {
                gearXPos = x;
                gearYPos = y - 1;
                return true;
            }
            else if(y > 0 && x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y - 1][x + 1], @"(\*)"))
            {
                gearXPos = x + 1;
                gearYPos = y - 1;
                return true;

            }
            else if(x > 0 && Regex.IsMatch(schematic[y][x - 1], @"(\*)"))
            {
                gearXPos = x - 1;
                gearYPos = y;
                return true;
            }
            else if (x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y][x + 1], @"(\*)"))
            {
                gearXPos = x + 1;
                gearYPos = y;
                return true;
            }
            else if (y < schematic.Length - 1 && x > 0 && Regex.IsMatch(schematic[y + 1][x - 1], @"(\*)"))
            {
                gearXPos = x - 1;
                gearYPos = y + 1;
                return true;
            }
            else if (y < schematic.Length - 1 && Regex.IsMatch(schematic[y + 1][x], @"(\*)"))
            {
                gearXPos = x;
                gearYPos = y + 1;
                return true;
            } 
            else if(y < schematic.Length - 1 && x < schematic[y].Length - 1 && Regex.IsMatch(schematic[y + 1][x + 1], @"(\*)"))
            {
                gearXPos = x + 1;
                gearYPos = y + 1;
                return true;
            }

            gearXPos = 0;
            gearYPos = 0;
            return false;
        }
    }

    public class Gear
    {
        public int PartId { get; init; }
        public int GearXPos { get; init; }
        public int GearYPos { get; init; }

        public Gear() { }

        public Gear(int partId, int gearXPos, int gearYPos)
        {
            PartId = partId;
            GearXPos = gearXPos;
            GearYPos = gearYPos;
        }
    }
}