using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_2 : DayBase<int>
    {
        public day_2() : base("day_02.txt") 
        {
            Console.WriteLine("Advent of Code - Day Two");
        }

        public day_2(string fileName) : base(fileName) { }

        private int sumOfPowers = 0;

        public override int Problem1()
        {
            Dictionary<int, Game> games = new Dictionary<int, Game>();
            int totalGameIds = 0;

            foreach (var input in _input)
            {
                int gameId;
                string gameString;

                RegexHelper.Match(input, @"Game (\d+): (.+)", out gameId, out gameString);

                // divide the gameString
                string[] gameStringParts = gameString.Split(';');

                int maxRedCount = 0;
                int maxBlueCount = 0;
                int maxGreenCount = 0;

                foreach(var gameStringPart in gameStringParts)
                {
                    string g = gameStringPart.Trim();

                    var pattern = new Regex(@"(\d+)\s*(blue|green|red)");

                    MatchCollection matches = pattern.Matches(g);

                    foreach (Match match in matches)
                    {
                        int quantity = int.Parse(match.Groups[1].Value);
                        string color = match.Groups[2].Value;

                        switch(color)
                        {
                            case "red":
                                if(quantity > maxRedCount) maxRedCount = quantity;
                                break;
                            case "blue":
                                if(quantity > maxBlueCount) maxBlueCount = quantity;
                                break;
                            case "green":
                                if(quantity > maxGreenCount) maxGreenCount = quantity;
                                break;  
                        }
                    }
                }

                if(maxRedCount <= 12 && maxGreenCount <= 13 && maxBlueCount <= 14)
                {
                    totalGameIds += gameId;
                }

                sumOfPowers += (maxBlueCount * maxGreenCount * maxRedCount);

                Game gameResults = new Game(gameId, maxRedCount, maxBlueCount, maxGreenCount); 
            }

            // only 12 red, 13 green, 14 blue
            return totalGameIds;

        }

        public override int Problem2()
        {
            if(sumOfPowers == 0)
            {
                Problem1();
            }   
            return sumOfPowers;
        }
    }

    public class Game
    {
        public int Id { get; init; }
        public int MaxRedCount { get; set; }
        public int MaxBlueCount { get; set; }
        public int MaxGreenCount { get; set; }
        public Game() { }

        public Game(int id, int maxRedCount, int maxBlueCount, int maxGreenCount)
        {
            Id = id;
            MaxRedCount = maxRedCount;
            MaxBlueCount = maxBlueCount;
            MaxGreenCount = maxGreenCount;
        }
    }
}
