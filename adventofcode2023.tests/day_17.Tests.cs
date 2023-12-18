using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DaySeventeenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_17("day_17_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(102));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_17("day_17_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(94));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_17();

            Assert.That(day.Problem1(), Is.EqualTo(1260));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_17();

            Assert.That(day.Problem2(), Is.EqualTo(1416));
        }
    }
}