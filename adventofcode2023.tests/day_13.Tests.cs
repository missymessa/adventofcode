using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayThirteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_13("day_13_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(405));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_13("day_13_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(400));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_13();

            Assert.That(day.Problem1(), Is.EqualTo(37561));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_13();

            Assert.That(day.Problem2(), Is.EqualTo(31108));
        }
    }
}