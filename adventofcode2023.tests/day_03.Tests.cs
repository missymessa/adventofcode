using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayThreeTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_3("day_03_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(4361));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_3("day_03_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(467835));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_3();

            Assert.That(day.Problem1(), Is.EqualTo(530495));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_3();

            Assert.That(day.Problem2(), Is.EqualTo(80253814));
        }
    }
}