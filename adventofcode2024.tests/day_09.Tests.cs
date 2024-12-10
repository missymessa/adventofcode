using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DayNineTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_9("day_09_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(1928));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_9("day_09_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(2858));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_9();

            Assert.That(day.Problem1(), Is.EqualTo(6225730762521));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_9();

            Assert.That(day.Problem2(), Is.EqualTo(6250605700557));
        }
    }
}