using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022.tests
{
    public class DaySixteenTests
    {
        [Test]
        public void Problem1Example()
        {
            var day = new DaySixteen("day_16_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(1351));
        }

        [Test]
        [Ignore("Not solved yet")]
        public void Problem2Example()
        {
            var day = new DaySixteen("day_16_ex.txt");

            Assert.That(day.Problem2(), Is.EqualTo(56000011));
        }

        [Test]
        [Ignore("Not solved yet")]
        public void Problem1Actual()
        {
            var day = new DaySixteen();

            Assert.That(day.Problem1(), Is.EqualTo(4951427));
        }

        [Test]
        [Ignore("Not solved yet")]
        public void Problem2Actual()
        {
            var day = new DaySixteen();

            //Assert.That(day.Problem2(), Is.EqualTo(199357));
        }
    }
}
