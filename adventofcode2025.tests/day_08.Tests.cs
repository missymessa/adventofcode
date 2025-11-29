using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayEightTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_08("day_08_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_08("day_08_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_08();

            Assert.That(day.Problem1(), Is.EqualTo(0));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_08();

            Assert.That(day.Problem2(), Is.EqualTo(0));
        }
    }
}
