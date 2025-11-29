using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayNineTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_09("day_09_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_09("day_09_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_09();

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_09();

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }
    }
}
