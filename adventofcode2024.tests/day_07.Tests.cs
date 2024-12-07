using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DaySevenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_7("day_07_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(3749));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_7("day_07_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(11387));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_7();

            Assert.That(day.Problem1(), Is.EqualTo(882304362421));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_7();

            Assert.That(day.Problem2(), Is.EqualTo(145149066755184));
        }
    }
}