using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DaySixTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_6("day_06_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(288));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_6("day_06_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(71503));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_6();

            Assert.That(day.Problem1(), Is.EqualTo(227850));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_6();

            Assert.That(day.Problem2(), Is.EqualTo(42948149));
        }
    }
}