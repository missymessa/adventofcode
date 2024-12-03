using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayThreeTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_3("day_03_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(161));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_3("day_03_ex2.txt");

            Assert.That(day.Problem2(), Is.EqualTo(48));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_3();

            Assert.That(day.Problem1(), Is.EqualTo(171183089));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_3();

            Assert.That(day.Problem2(), Is.EqualTo(63866497));
        }
    }
}