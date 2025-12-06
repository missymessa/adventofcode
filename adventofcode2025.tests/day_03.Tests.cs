using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayThreeTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_03("day_03_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(357));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_03("day_03_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(3121910778619));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_03();

            Assert.That(day.Problem1(), Is.EqualTo(16854));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_03();

            Assert.That(day.Problem2(), Is.EqualTo(167526011932478));
        }
    }
}
