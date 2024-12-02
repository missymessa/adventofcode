using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayTwoTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_2("day_02_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(2));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_2("day_02_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(4));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_2();

            Assert.That(day.Problem1(), Is.EqualTo(680));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_2();

            Assert.That(day.Problem2(), Is.EqualTo(710));
        }
    }
}