using System;
using System.Collections;
using System.Collections.Generic;

namespace adventofcode2020
{
    public static class DayTwentyThree
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        public static void ProblemOne()
        {
            Hashtable cups = ProblemOneSetup();

            int currentCup = 5;
            int numberOfIterations = 100;

            for(int i = 0; i < numberOfIterations; i++)
            {
                // calculate destination cup
                int destinationCup = (currentCup - 1 == 0) ? 9 : currentCup - 1;

                // pick up 3 cups
                List<int> cupsPickedUp = new List<int>();
                int pickUpCup = (int)cups[currentCup];
                cupsPickedUp.Add(pickUpCup);
                cupsPickedUp.Add((int)cups[pickUpCup]);
                int lastPickUpCup = (int)cups[(int)cups[pickUpCup]];
                cupsPickedUp.Add(lastPickUpCup);
                int nextCup = (int)cups[lastPickUpCup];

                // reassign current cup's next cup
                cups[currentCup] = nextCup;

                // look for cup to put the pick up cups next to
                while(cupsPickedUp.Contains(destinationCup))
                {
                    if(--destinationCup == 0)
                    {
                        destinationCup = 9;
                    }
                } 

                int insertBefore = (int)cups[destinationCup];
                cups[destinationCup] = pickUpCup;
                cups[lastPickUpCup] = insertBefore;

                // select new current cup
                currentCup = (int)cups[currentCup];
            }

            ProblemOneAnswer(cups);
        }

        public static void ProblemTwo()
        {
            Hashtable cups = ProblemTwoSetup();

            int currentCup = 5;
            int numberOfIterations = 10000000;

            for (int i = 0; i < numberOfIterations; i++)
            {
                // calculate destination cup
                int destinationCup = (currentCup - 1 == 0) ? 1000000 : currentCup - 1;

                // pick up 3 cups
                List<int> cupsPickedUp = new List<int>();
                int pickUpCup = (int)cups[currentCup];
                cupsPickedUp.Add(pickUpCup);
                cupsPickedUp.Add((int)cups[pickUpCup]);
                int lastPickUpCup = (int)cups[(int)cups[pickUpCup]];
                cupsPickedUp.Add(lastPickUpCup);
                int nextCup = (int)cups[lastPickUpCup];

                // reassign current cup's next cup
                cups[currentCup] = nextCup;

                // look for cup to put the pick up cups next to
                while (cupsPickedUp.Contains(destinationCup))
                {
                    if (--destinationCup == 0)
                    {
                        destinationCup = 1000000;
                    }
                }

                int insertBefore = (int)cups[destinationCup];
                cups[destinationCup] = pickUpCup;
                cups[lastPickUpCup] = insertBefore;

                // select new current cup
                currentCup = (int)cups[currentCup];
            }

            ProblemTwoAnswer(cups);
        }

        private static Hashtable ProblemOneSetup()
        {
            // Example
            /*
            Hashtable cups = new Hashtable();
            cups.Add(3, 8);
            cups.Add(8, 9);
            cups.Add(9, 1);
            cups.Add(1, 2);
            cups.Add(2, 5);
            cups.Add(5, 4);
            cups.Add(4, 6);
            cups.Add(6, 7);
            cups.Add(7, 3);*/
            
            Hashtable cups = new Hashtable();
            cups.Add(5, 8);
            cups.Add(8, 6);
            cups.Add(6, 4);
            cups.Add(4, 3);
            cups.Add(3, 9);
            cups.Add(9, 1);
            cups.Add(1, 7);
            cups.Add(7, 2);
            cups.Add(2, 5);

            return cups;
        }

        private static Hashtable ProblemTwoSetup()
        {
            // Example
            /*
            Hashtable cups = new Hashtable();
            cups.Add(3, 8);
            cups.Add(8, 9);
            cups.Add(9, 1);
            cups.Add(1, 2);
            cups.Add(2, 5);
            cups.Add(5, 4);
            cups.Add(4, 6);
            cups.Add(6, 7);
            int lastCup = 7;
            int firstCup = 3;
            */
            
            Hashtable cups = new Hashtable();
            cups.Add(5, 8);
            cups.Add(8, 6);
            cups.Add(6, 4);
            cups.Add(4, 3);
            cups.Add(3, 9);
            cups.Add(9, 1);
            cups.Add(1, 7);
            cups.Add(7, 2);
            int lastCup = 2;
            int firstCup = 5;

            int iterator = 10; 

            while(iterator <= 1000000)
            {
                cups.Add(lastCup, iterator);
                lastCup = iterator++;
            }

            cups.Add(lastCup, firstCup);

            return cups;
        }

        private static void ProblemOneAnswer(Hashtable cups)
        {
            int iterator = 1;
            int nextCup = (int)cups[iterator];

            string condensedCupString = "";

            while (iterator < cups.Count)
            {
                condensedCupString += nextCup.ToString();
                nextCup = (int)cups[nextCup];
                iterator++;
            }

            Console.WriteLine($"Current order of cups after 1: {condensedCupString}");
        }

        private static void ProblemTwoAnswer(Hashtable cups)
        {
            long firstNextCup = Convert.ToInt64(cups[1]);
            long secondNextCup = Convert.ToInt64(cups[(int)firstNextCup]);

            Console.WriteLine($"Product of next two cups: {firstNextCup} * {secondNextCup} = {firstNextCup * secondNextCup}");
        }
    }
}
