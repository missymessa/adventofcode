using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DaySixTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_6("day_06_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(41));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_6("day_06_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(6));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_6();

            Assert.That(day.Problem1(), Is.EqualTo(5531));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_6();

            Assert.That(day.Problem2(), Is.EqualTo(2165));
        }
    }
}