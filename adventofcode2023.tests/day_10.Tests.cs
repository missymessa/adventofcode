using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayTenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_10("day_10_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(8));
        }

        [Test]
        [TestCase("day_10_ex2.txt", 4)]
        [TestCase("day_10_ex3.txt", 4)]
        [TestCase("day_10_ex4.txt", 8)]
        [TestCase("day_10_ex5.txt", 10)]
        public void Problem2Example(string filename, long result)
        {
            var day = new day_10(filename);

            Assert.That(day.Problem2(), Is.EqualTo(result));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_10();

            Assert.That(day.Problem1(), Is.EqualTo(7107));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_10();

            Assert.That(day.Problem2(), Is.EqualTo(281));
        }
    }
}