using adventofcode2023;
using System.Numerics;

namespace adventofcode2023.tests
{
    public class DayNineteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_19("day_19_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(19114));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_19("day_19_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(167409079868000));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_19();

            Assert.That(day.Problem1(), Is.EqualTo(398527));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_19();

            Assert.That(day.Problem2(), Is.EqualTo(133973513090020));
        }
    }
}