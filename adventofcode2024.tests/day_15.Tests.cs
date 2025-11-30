using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayFifteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_15("day_15_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(10092));
        }

        [Test]
        public void Problem1Example2()
        {
            var day = new day_15("day_15_ex2.txt");

            Assert.That(day.Problem1(), Is.EqualTo(2028));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_15("day_15_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(9021));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_15();

            Assert.That(day.Problem1(), Is.EqualTo(1436690));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_15();

            Assert.That(day.Problem2(), Is.EqualTo(108713182988244));
        }
    }
}