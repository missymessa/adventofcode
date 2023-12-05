using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayFiveTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_5("day_05_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(35));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_5("day_05_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(46));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_5();

            Assert.That(day.Problem1(), Is.EqualTo(331445006));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_5();

            Assert.That(day.Problem2(), Is.EqualTo(6472060));
        }
    }
}