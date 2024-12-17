using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayFourteenTests
    {
        [Test]
        [Ignore("Example is a different size than actual")]
        public void Problem1Example()
        {
            var day = new day_14("day_14_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(12));
        }

        [Test]
        [Ignore("No problem 2 example")]
        public void Problem2Example()
        {
            var day = new day_14("day_14_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(1206));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_14();

            Assert.That(day.Problem1(), Is.EqualTo(221142636));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_14();

            Assert.That(day.Problem2(), Is.EqualTo(7916));
        }
    }
}