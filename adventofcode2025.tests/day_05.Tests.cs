using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayFiveTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_05("day_05_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_05("day_05_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_05();

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_05();

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }
    }
}
