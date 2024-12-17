using adventofcode2024;

namespace adventofcode2024.tests
{
    public class DaySixteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_16("day_16_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(7036));
        }

        [Test]
        public void Problem1SecondExample()
        {
            var day = new day_16("day_16_ex2.txt");

            Assert.That(day.Problem1(), Is.EqualTo(11048));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_16("day_16_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(1206));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_16();

            Assert.That(day.Problem1(), Is.EqualTo(26810));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_16();

            Assert.That(day.Problem2(), Is.EqualTo(108713182988244));
        }
    }
}