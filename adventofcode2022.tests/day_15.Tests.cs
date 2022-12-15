using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022.tests
{
    public class DayFifteenTests
    {
        [Test]
        public void SensorBeaconPairTest()
        {
            var sensorBeaconPair = new SensorBeaconPair((7, 8), (10, 2));

            Assert.That(sensorBeaconPair.GetPointSpreadForRow(10), Is.EqualTo((2, 14)));
        }

        [Test]
        public void Problem1Example()
        {
            var day = new DayFifteen(10, "day_15_ex.txt");

            Assert.That(day.Problem1(), Is.EqualTo(26));
        }

        [Test]
        public void Problem2Example()
        {
            var day = new DayFifteen(10, "day_15_ex.txt");

            //Assert.That(day.Problem2(), Is.EqualTo(45000));
        }

        [Test]
        public void Problem1Actual()
        {
            var day = new DayFifteen();

            Assert.That(day.Problem1(), Is.EqualTo(4951427));
        }

        [Test]
        public void Problem2Actual()
        {
            var day = new DayFifteen();

            //Assert.That(day.Problem2(), Is.EqualTo(199357));
        }
    }
}
