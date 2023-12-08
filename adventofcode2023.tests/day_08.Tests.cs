using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayEightTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_8("day_08_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(6));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_8("day_08_ex2.txt");

            Assert.That(day.Problem2(), Is.EqualTo(6));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_8();

            Assert.That(day.Problem1(), Is.EqualTo(17287));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_8();

            Assert.That(day.Problem2(), Is.EqualTo(18625484023687));
        }
    }
}