using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayThirteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_13("day_13_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(480));
        }

        [Test]
        [Ignore("Part 2 does not have an example")]
        public void Problem2Example()
        {
            var day = new day_13("day_13_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(1206));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_13();

            Assert.That(day.Problem1(), Is.EqualTo(26810));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_13();

            Assert.That(day.Problem2(), Is.EqualTo(108713182988244));
        }
    }
}