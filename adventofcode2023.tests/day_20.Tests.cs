using adventofcode2023;
using System.Numerics;

namespace adventofcode2023.tests
{
    public class DayTwentyTests
    {
        [Test]
        [TestCase("day_20_ex.txt", 32000000)]
        [TestCase("day_20_ex2.txt", 11687500)]
        public void Problem1Example(string filename, long result)
        {
            var day = new day_20(filename);

            Assert.That(day.Problem1(), Is.EqualTo(result));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_20();

            Assert.That(day.Problem1(), Is.EqualTo(883726240));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_20();

            Assert.That(day.Problem2(), Is.EqualTo(211712400442661));
        }
    }
}