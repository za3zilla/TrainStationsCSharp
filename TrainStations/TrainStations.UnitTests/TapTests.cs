using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainStations.Entities;

namespace TrainStations.UnitTests
{
    [TestClass]
    public class TapTests
    {
        [TestMethod]
        public void TapShouldHaveData()
        {
            //Create new Tap
            int unixTimestamp = 1;
            int customerId = 1;
            string station = "A";
            Tap tap = new Tap(unixTimestamp, customerId, station);

            //New Tap created
            Assert.IsNotNull(tap);

            //Check that the tap has the right unixTimestamp
            Assert.AreEqual(unixTimestamp, tap.UnixTimestamp);

            //Check that the tap has the right customerId
            Assert.AreEqual(customerId, tap.CustomerId);

            //Check that the tap has the right station
            Assert.AreEqual(station, tap.Station);
        }
    }
}
