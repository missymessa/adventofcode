using adventofcode2022;
using NUnit.Framework;

namespace adventofcode2022.tests
{
    public class DayOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new DayOne("day_01_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(24000));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new DayOne("day_01_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(45000));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new DayOne();

            Assert.That(day.Problem1(), Is.EqualTo(67450));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new DayOne();

            Assert.That(day.Problem2(), Is.EqualTo(199357));
        }
    }
}