using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayFourTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_4("day_04_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(18));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_4("day_04_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(9));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_4();

            Assert.That(day.Problem1(), Is.EqualTo(2344));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_4();

            Assert.That(day.Problem2(), Is.EqualTo(1815));
        }
    }
}