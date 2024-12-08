using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayEightTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_8("day_08_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(14));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_8("day_08_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(34));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_8();

            Assert.That(day.Problem1(), Is.EqualTo(400));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_8();

            Assert.That(day.Problem2(), Is.EqualTo(1280));
        }
    }
}