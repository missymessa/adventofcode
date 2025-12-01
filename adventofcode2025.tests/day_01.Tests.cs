using adventofcode2025;

namespace adventofcode2025.tests
{
    public class DayOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_01("day_01_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(3));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_01("day_01_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(6));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_01();

            Assert.That(day.Problem1(), Is.EqualTo(1097));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_01();

            Assert.That(day.Problem2(), Is.EqualTo(7101));
        }
    }
}
