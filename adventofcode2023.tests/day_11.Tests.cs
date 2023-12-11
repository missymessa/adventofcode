using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayElevenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_11("day_11_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(374));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_11("day_11_ex.txt");

            Assert.That(day.DoProblem(99), Is.EqualTo(8410));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_11();

            Assert.That(day.Problem1(), Is.EqualTo(9274989));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_11();

            Assert.That(day.Problem2(), Is.EqualTo(357134560737));
        }

        [Test]
        [TestCase(4, 0, 9, 10, 15)] // Between galaxy 1 and galaxy 7: 15
        [TestCase(0, 2, 12, 7, 17)] // Between galaxy 3 and galaxy 6: 17
        [TestCase(0, 11, 5, 11, 5)] // Between galaxy 8 and galaxy 9: 5
        [TestCase(1, 6, 5, 11, 9)]  // Between galaxy 5 and galaxy 9: 9
        public void CalculateDistance(int x1, int y1, int x2, int y2, int result)
        {
            var day = new day_11();

            Assert.That(day.CalculateDistance((x1, y1), (x2, y2)), Is.EqualTo(result));
        }
    }
}