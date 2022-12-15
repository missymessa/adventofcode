using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022.tests
{
    public class DayTwoTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new DayTwo("day_02_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(15));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new DayTwo("day_02_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(12));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new DayTwo();

            Assert.That(day.Problem1(), Is.EqualTo(17189));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new DayTwo();

            Assert.That(day.Problem2(), Is.EqualTo(13490));
        }
    }
}
