using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayFourTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_4("day_04_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(13));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_4("day_04_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(30));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_4();

            Assert.That(day.Problem1(), Is.EqualTo(33950));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_4();

            Assert.That(day.Problem2(), Is.EqualTo(14814534));
        }
    }
}