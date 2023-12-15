using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayFifteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_15("day_15_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(1320));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_15("day_15_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(145));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_15();

            Assert.That(day.Problem1(), Is.EqualTo(510388));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_15();

            Assert.That(day.Problem2(), Is.EqualTo(291774));
        }
    }
}