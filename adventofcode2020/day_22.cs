using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayTwentyTwo
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        public static void ProblemOne()
        {
            (Queue<int> player1, Queue<int> player2) = Setup();

            while (player1.Count > 0 && player2.Count > 0)
            {
                int player1Card = player1.Dequeue();
                int player2Card = player2.Dequeue();

                if(player1Card > player2Card)
                {
                    player1.Enqueue(player1Card);
                    player1.Enqueue(player2Card);
                }
                else
                {
                    player2.Enqueue(player2Card);
                    player2.Enqueue(player1Card);
                }
            }

            // Score
            Score(player1, player2);
        }

        public static void ProblemTwo()
        {
            (Queue<int> player1, Queue<int> player2) = Setup();

            int exitCode = 0;
            while (player1.Count > 0 && player2.Count > 0 && exitCode == 0)
            {
                exitCode = PlayGame(player1, player2);
            }

            // Score
            Score(player1, player2);
        }

        private static int PlayGame(Queue<int> player1, Queue<int> player2)
        {
            List<string> player1DeckSnapshots = new List<string>();
            List<string> player2DeckSnapshots = new List<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                // check for weird rule
                string player1CurrentDeckSnapshot = string.Join(",", player1);
                string player2CurrentDeckSnapshot = string.Join(",", player2);

                if (player1DeckSnapshots.Contains(player1CurrentDeckSnapshot) && player2DeckSnapshots.Contains(player2CurrentDeckSnapshot))
                {
                    return 1;
                }

                // add snapshot
                player1DeckSnapshots.Add(player1CurrentDeckSnapshot);
                player2DeckSnapshots.Add(player2CurrentDeckSnapshot);

                int player1Card = player1.Dequeue();
                int player2Card = player2.Dequeue();

                if (player1.Count > 0 && player2.Count > 0 && player1.Count >= player1Card && player2.Count >= player2Card)
                {
                    int winningPlayer = PlayGame(new Queue<int>(player1.Take(player1Card)), new Queue<int>(player2.Take(player2Card)));

                    if (winningPlayer == 1)
                    {
                        player1.Enqueue(player1Card);
                        player1.Enqueue(player2Card);
                    }
                    else if (winningPlayer == 2)
                    {
                        player2.Enqueue(player2Card);
                        player2.Enqueue(player1Card);
                    }
                }
                else
                {
                    if (player1Card > player2Card)
                    {
                        player1.Enqueue(player1Card);
                        player1.Enqueue(player2Card);
                    }
                    else
                    {
                        player2.Enqueue(player2Card);
                        player2.Enqueue(player1Card);
                    }
                }
            }

            if (player1.Count > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private static (Queue<int> player1, Queue<int> player2) Setup()
        {
            Queue<string> input = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_22.txt")).ToList());
            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();

            int playerSwitch = 1;
            while (input.Count > 0)
            {
                if (input.Peek().StartsWith("Player"))
                {
                    var player = input.Dequeue();
                    if (player == "Player 2:")
                    {
                        playerSwitch = 2;
                    }
                }
                else if (input.Peek().Length == 0)
                {
                    input.Dequeue();
                }
                else
                {
                    if (playerSwitch == 1)
                    {
                        player1.Enqueue(int.Parse(input.Dequeue()));
                    }
                    else
                    {
                        player2.Enqueue(int.Parse(input.Dequeue()));
                    }
                }
            }

            return (player1, player2);
        }

        private static void Score(Queue<int> player1, Queue<int> player2)
        {
            Queue<int> playerToScore = new Queue<int>();
            if (player1.Count > 0)
            {
                playerToScore = player1;
            }
            else
            {
                playerToScore = player2;
            }

            int score = 0;
            int iteration = playerToScore.Count;
            while (playerToScore.Count > 0)
            {
                score += (playerToScore.Dequeue() * iteration--);
            }

            Console.WriteLine($"Winning Player's Score: {score}");
        }
    }
}
