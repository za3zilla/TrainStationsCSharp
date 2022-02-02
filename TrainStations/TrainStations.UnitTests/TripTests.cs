using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainStations.Entities;

namespace TrainStations.UnitTests
{
    [TestClass]
    public class TripTests
    {
        [TestMethod]
        public void TripShouldHaveData()
        {
            //Create new Trip
            string stationStart = "A";
            string stationEnd = "D";
            int startedJourneyAt = 1;
            decimal cost = 1.2m;
            int zoneFrom = 1;
            int zoneTo = 2;
            Trip trip = new Trip(stationStart, stationEnd, startedJourneyAt, cost, zoneFrom, zoneTo);

            //New Trip created
            Assert.IsNotNull(trip);

            //Check that the trip has the right stationStart
            Assert.AreEqual(stationStart, trip.StationStart);

            //Check that the trip has the right stationEnd
            Assert.AreEqual(stationEnd, trip.StationEnd);

            //Check that the trip has the right startedJourneyAt
            Assert.AreEqual(startedJourneyAt, trip.StartedJourneyAt);

            //Check that the trip has the right cost
            Assert.AreEqual(cost, trip.Cost);

            //Check that the trip has the right zoneFrom
            Assert.AreEqual(zoneFrom, trip.ZoneFrom);

            //Check that the trip has the right zoneTo
            Assert.AreEqual(zoneTo, trip.ZoneTo);
        }
    }
}
