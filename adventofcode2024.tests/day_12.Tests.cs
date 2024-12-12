using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayTwelveTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_12("day_12_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(1930));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_12("day_12_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(1206));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_12();

            Assert.That(day.Problem1(), Is.EqualTo(1494342));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_12();

            Assert.That(day.Problem2(), Is.EqualTo(893676));
        }
    }
}