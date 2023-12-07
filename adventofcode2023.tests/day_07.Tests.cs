using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DaySevenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_7("day_07_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(6440));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_7("day_07_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(5905));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_7();

            Assert.That(day.Problem1(), Is.EqualTo(249204891));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_7();

            Assert.That(day.Problem2(), Is.EqualTo(249666369));
        }

        [Test]
        [TestCase("32T3K", 3)]
        [TestCase("KK677", 4)]
        [TestCase("KTJJT", 4)]
        [TestCase("T55J5", 5)]
        [TestCase("QQQJA", 5)]
        public void CamelHandComparerHandStrength(string hand, int result)
        {
            var comparer = new CamelHandComparer();

            Assert.That(comparer.HandStrength(hand), Is.EqualTo(result));
        }

        [Test]
        [TestCase("32T3K", 3)]
        [TestCase("KK677", 4)]
        [TestCase("KTJJT", 7)]
        [TestCase("T55J5", 7)]
        [TestCase("QQQJA", 7)]
        [TestCase("JJJJJ", 8)]
        public void CamelHandWithJokersComparerHandStrength(string hand, int result)
        {
            var comparer = new CamelHandWithJokersComparer();

            Assert.That(comparer.HandStrength(hand), Is.EqualTo(result));
        }
    }
}