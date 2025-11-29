using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayFourTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_04("day_04_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_04("day_04_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_04();

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_04();

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }
    }
}
