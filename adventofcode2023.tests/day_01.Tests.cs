using adventofcode2023;

namespace adventofcode2023.tests
{
    public class DayOneTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new day_1("day_01_ex.txt");

            //Assert.That(day.Problem1(), Is.EqualTo(24000));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new day_1("day_01_ex.txt");

            //Assert.That(day.Problem2(), Is.EqualTo(45000));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new day_1();

            //Assert.That(day.Problem1(), Is.EqualTo(67450));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new day_1();

           // Assert.That(day.Problem2(), Is.EqualTo(199357));
        }
    }
}