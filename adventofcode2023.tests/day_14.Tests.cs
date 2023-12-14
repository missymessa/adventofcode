using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayFourteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_14("day_14_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(136));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_14("day_14_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(64));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_14();

            Assert.That(day.Problem1(), Is.EqualTo(112048));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_14();

            Assert.That(day.Problem2(), Is.EqualTo(105606));
        }
    }
}