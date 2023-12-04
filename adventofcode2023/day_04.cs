using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_4 : DayBase<int>
    {
        public day_4() : base("day_04.txt")
        {
            Console.WriteLine("Advent of Code - Day Four");
        }

        public day_4(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            int totalScore = 0;

            foreach (var input in _input)
            {
                // parse input
                Match m = RegexHelper.GetMatch(input, @"^Card\s+\d+: (.+)", 1);
                var cardContents = m.Groups[1].Value.Split("|");
                var winningNumbers = cardContents[0].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                var cardNumbers = cardContents[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                // find matches
                int numberOfMatches = winningNumbers.Intersect(cardNumbers).Count();

                // determine score
                int score = 1 * (int)Math.Pow(2, numberOfMatches - 1);

                totalScore += score;
            }

            // return sum of scores
            return totalScore;
        }

        public override int Problem2()
        {
            Queue<int> cardIdsInPossession = new Queue<int>();
            Dictionary<int, int> matchesPerCard = new Dictionary<int, int>();
            int totalNumberOfCardsScratched = 0;

            foreach (var input in _input)
            {
                // parse input
                Match m = RegexHelper.GetMatch(input, @"^Card\s+\d+: (.+)", 1);
                var cardId = Convert.ToInt32(RegexHelper.GetMatch(m.Groups[0].Value, @"^Card\s+(\d+):", 1).Groups[1].Value);
                var cardContents = m.Groups[1].Value.Split("|");
                var winningNumbers = cardContents[0].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                var cardNumbers = cardContents[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                // find matches
                int numberOfMatches = winningNumbers.Intersect(cardNumbers).Count();

                matchesPerCard.Add(cardId, numberOfMatches);

                // remember to add the original cards to the queue!
                cardIdsInPossession.Enqueue(cardId);
            }

            while(cardIdsInPossession.Count > 0)
            {
                // dequeue card
                var card = cardIdsInPossession.Dequeue();

                // enqueue cards equal to number of matches on dequeued card
                for(int i = 1; i <= matchesPerCard[card]; i++)
                {
                    cardIdsInPossession.Enqueue(card + i);
                }

                totalNumberOfCardsScratched++;
            }

            return totalNumberOfCardsScratched;
        }
    }
}