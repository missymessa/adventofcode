using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayNineTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_9("day_09_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(114));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_9("day_09_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(2));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_9();

            Assert.That(day.Problem1(), Is.EqualTo(1969958987));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_9();

            Assert.That(day.Problem2(), Is.EqualTo(1068));
        }

        [Test]
        [TestCase("2 9 16 15 -9 -83 -246 -533 -925 -1242 -961 1048 6903 20444 49221 110304 246066 563097 1320347 3115736 7261163", 16506703)]
        [TestCase("12 26 45 62 75 102 213 596 1688 4420 10644 23834 50207 100575 193732 363513 674924 1262058 2415742 4778607 9760269", 20382238)]
        public void TestExtrapolate(string input, int result)
        {
            var day = new day_9();

            Assert.That(day.Extrapolate(input), Is.EqualTo(result));
        }
    }
}