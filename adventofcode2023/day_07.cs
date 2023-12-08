using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_7 : DayBase<long>
    {
        public day_7() : base("day_07.txt")
        {
            Console.WriteLine("Advent of Code - Day Seven");
        }

        public day_7(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            List<(string hand, int bid)> hands = _input.Select(line =>
                {
                    string[] parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    return (parts[0], int.Parse(parts[1]));
                }).ToList();

            var customComparer = new CamelHandComparer();
            hands.Sort(customComparer);

            long totalWinnings = 0;
            int rank = 1;

            for(int i = hands.Count - 1; i >= 0; i--)
            {
                totalWinnings += (rank * hands[i].bid);
                rank++;
            }

            return totalWinnings;
        }

        public override long Problem2()
        {
            List<(string hand, int bid)> hands = _input.Select(line =>
            {
                string[] parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return (parts[0], int.Parse(parts[1]));
            }).ToList();

            var customComparer = new CamelHandWithJokersComparer();
            hands.Sort(customComparer);

            long totalWinnings = 0;
            int rank = 1;

            for (int i = hands.Count - 1; i >= 0; i--)
            {
                totalWinnings += (rank * hands[i].bid);
                rank++;
            }

            return totalWinnings;
        }
    }

    public class CamelHandComparer : IComparer<(string, int)>
    {
        public int Compare((string, int) x, (string, int) y)
        {
            // Compare based on hand strength first
            int strengthComparison = HandStrength(x.Item1).CompareTo(HandStrength(y.Item1));

            // If hands have different strength, return the result of the strength comparison
            if (strengthComparison != 0)
            {
                return -strengthComparison; // Invert for descending order
            }

            // If hands have the same strength, compare the values of the cards
            var xCardValues = x.Item1.Select(CardValue).ToList();
            var yCardValues = y.Item1.Select(CardValue).ToList();

            for (int i = 0; i < Math.Min(xCardValues.Count, yCardValues.Count); i++)
            {
                int elementComparison = xCardValues[i].CompareTo(yCardValues[i]);
                if (elementComparison != 0)
                {
                    return elementComparison; // Invert for descending order
                }
            }

            // If all elements are equal, maintain their original order in the list
            return 0;
        }

        private int CardValue(char card)
        {
            // Assign a numeric value to each card label
            string cardLabels = "AKQJT98765432";
            return cardLabels.IndexOf(card);
        }

        public int HandStrength(string hand)
        {
            // Parse the hand and extract the card labels
            List<char> cards = hand.ToList();

            // Count occurrences of each card label
            Dictionary<char, int> cardCounts = cards.Distinct().ToDictionary(card => card, card => cards.Count(c => c == card));

            // Sort the cards by count and value
            List<char> sortedCards = cards.OrderByDescending(card => (cardCounts[card], CardValue(card))).ToList();

            // Check for each hand type and return its strength
            if (cardCounts.Values.All(count => count == 5))
            {
                return 8;  // Five of a kind
            }
            else if (cardCounts.ContainsValue(4))
            {
                return 7;  // Four of a kind
            }
            else if (cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2))
            {
                return 6;  // Full house
            }
            else if (cardCounts.ContainsValue(3))
            {
                return 5;  // Three of a kind
            }
            else if (cardCounts.Values.Count(x => x == 2) == 2 && cardCounts.ContainsValue(2))
            {
                return 4;  // Two pair
            }
            else if (cardCounts.ContainsValue(2))
            {
                return 3;  // One pair
            }
            else
            {
                return 2;  // High card
            }
        }
    }

    public class CamelHandWithJokersComparer : IComparer<(string, int)>
    {
        public int Compare((string, int) x, (string, int) y)
        {
            // Compare based on hand strength first
            int strengthComparison = HandStrength(x.Item1).CompareTo(HandStrength(y.Item1));

            // If hands have different strength, return the result of the strength comparison
            if (strengthComparison != 0)
            {
                return -strengthComparison; // Invert for descending order
            }

            // If hands have the same strength, compare the values of the cards
            var xCardValues = x.Item1.Select(CardValue).ToList();
            var yCardValues = y.Item1.Select(CardValue).ToList();

            for (int i = 0; i < Math.Min(xCardValues.Count, yCardValues.Count); i++)
            {
                int elementComparison = xCardValues[i].CompareTo(yCardValues[i]);
                if (elementComparison != 0)
                {
                    return elementComparison; // Invert for descending order
                }
            }

            // If all elements are equal, maintain their original order in the list
            return 0;
        }

        private int CardValue(char card)
        {
            // Assign a numeric value to each card label
            string cardLabels = "AKQT98765432J";
            return cardLabels.IndexOf(card);
        }

        public int HandStrength(string hand)
        { 
            // Parse the hand and extract the card labels
            List<char> cards = hand.ToList();
            Dictionary<char, int> originalCardCounts = cards.Distinct().ToDictionary(card => card, card => cards.Count(c => c == card));

            List<string> newHands = new List<string>();

            // check to see if there are any jokers, if so, replace with other cards in the hand and see which is larger
            foreach(var occ in originalCardCounts)
            {
                newHands.Add(hand.Replace('J', occ.Key));
            }

            int highestValue = 0;

            foreach (var newHand in newHands)
            {
                cards = newHand.ToList();

                // Count occurrences of each card label
                Dictionary<char, int> cardCounts = cards.Distinct().ToDictionary(card => card, card => cards.Count(c => c == card));

                // Sort the cards by count and value
                List<char> sortedCards = cards.OrderByDescending(card => (cardCounts[card], CardValue(card))).ToList();

                // Check for each hand type and return its strength
                if (cardCounts.Values.All(count => count == 5))
                {
                    if(highestValue < 8) highestValue = 8;  // Five of a kind
                }
                else if (cardCounts.ContainsValue(4))
                {
                    if (highestValue < 7) highestValue = 7;  // Four of a kind
                }
                else if (cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2))
                {
                    if (highestValue < 6) highestValue = 6;  // Full house
                }
                else if (cardCounts.ContainsValue(3))
                {
                    if (highestValue < 5) highestValue = 5;  // Three of a kind
                }
                else if (cardCounts.Values.Count(x => x == 2) == 2 && cardCounts.ContainsValue(2))
                {
                    if (highestValue < 4) highestValue = 4;  // Two pair
                }
                else if (cardCounts.ContainsValue(2))
                {
                    if (highestValue < 3) highestValue = 3;  // One pair
                }
                else
                {
                    if (highestValue < 2) highestValue = 2;  // High card
                }
            }

            return highestValue;
        }
    }
}