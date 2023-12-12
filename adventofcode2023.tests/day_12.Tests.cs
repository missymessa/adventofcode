using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayTwelveTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_12("day_12_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(21));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_12("day_12_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(525152));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_12();

            Assert.That(day.Problem1(), Is.EqualTo(7407));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_12();

            Assert.That(day.Problem2(), Is.EqualTo(30568243604962));
        }
    }
}