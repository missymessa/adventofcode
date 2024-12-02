using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_1("day_01_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(11));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_1("day_01_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(31));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_1();

            Assert.That(day.Problem1(), Is.EqualTo(2164381));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_1();

            Assert.That(day.Problem2(), Is.EqualTo(20719933));
        }
    }
}