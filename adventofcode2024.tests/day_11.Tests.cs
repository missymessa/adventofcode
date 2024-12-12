using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayElevenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_11("day_11_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(55312));
        }

        [Test]
        [Ignore("There is no test for this example in part 2")]
        public void Problem2Example()
        {
            var day = new day_11("day_11_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(81));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_11();

            Assert.That(day.Problem1(), Is.EqualTo(231278));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_11();

            Assert.That(day.Problem2(), Is.EqualTo(274229228071551));
        }
    }
}