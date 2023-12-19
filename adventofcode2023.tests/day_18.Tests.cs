using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayEighteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_18("day_18_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(62));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_18("day_18_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(952408144115));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_18();

            Assert.That(day.Problem1(), Is.EqualTo(28911));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_18();

            Assert.That(day.Problem2(), Is.EqualTo(77366737561114));
        }
    }
}