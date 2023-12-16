using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DaySixteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_16("day_16_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(46));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_16("day_16_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(51));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_16();

            Assert.That(day.Problem1(), Is.EqualTo(6514));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_16();

            Assert.That(day.Problem2(), Is.EqualTo(8089));
        }
    }
}