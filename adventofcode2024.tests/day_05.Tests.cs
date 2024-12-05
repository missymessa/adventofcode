using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayFiveTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_5("day_05_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(143));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_5("day_05_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(123));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_5();

            Assert.That(day.Problem1(), Is.EqualTo(7024));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_5();

            Assert.That(day.Problem2(), Is.EqualTo(4151));
        }
    }
}