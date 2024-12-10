using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayTenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_10("day_10_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(36));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_10("day_10_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(81));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_10();

            Assert.That(day.Problem1(), Is.EqualTo(796));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_10();

            Assert.That(day.Problem2(), Is.EqualTo(1942));
        }
    }
}