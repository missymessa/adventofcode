using adventofcode2023;
using System.Numerics;

namespace adventofcode2023.tests
{
    public class DayTwentyOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_21("day_21_ex.txt", 6);

            Assert.That(day.Problem1(), Is.EqualTo(16));
        }

        [Test]
        [TestCase(6, 16)]
        [TestCase(10, 50)]
        [TestCase(50, 1594)]
        [TestCase(100, 6536)]
        [TestCase(500, 167004)]
        [TestCase(1000, 668697)]
        [TestCase(5000, 16733044)]
        public void Problem2Example(long stepsRemaining, long expectedPlotsReached)
        {
            var day = new day_21("day_21_ex.txt", stepsRemaining);

            Assert.That(day.Problem2(), Is.EqualTo(expectedPlotsReached));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_21();

            Assert.That(day.Problem1(), Is.EqualTo(3795));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_21();

            Assert.That(day.Problem2(), Is.EqualTo(133973513090020));
        }
    }
}