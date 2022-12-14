using adventofcode2022; 

namespace adventofcode2022.tests
{
    public class DayOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var dayOne = new DayOne("day_01_ex.txt");

            Assert.AreEqual(24000, dayOne.Problem1());
        }

        [Test]
        public void Problem2Example()
        {
            var dayOne = new DayOne("day_01_ex.txt");

            Assert.AreEqual(45000, dayOne.Problem2());
        }

        [Test]
        public void Problem1Actual()
        {
            var dayOne = new DayOne("day_01.txt");

            Assert.AreEqual(67450, dayOne.Problem1());
        }

        [Test]
        public void Problem2Actual()
        {
            var dayOne = new DayOne("day_01.txt");

            Assert.AreEqual(199357, dayOne.Problem2());
        }
    }
}