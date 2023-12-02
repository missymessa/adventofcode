using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayTwoTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_2("day_02_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(8));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_2("day_02_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(2286));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_2();

            Assert.That(day.Problem1(), Is.EqualTo(2061));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_2();

            Assert.That(day.Problem2(), Is.EqualTo(72596));
        }
    }
}