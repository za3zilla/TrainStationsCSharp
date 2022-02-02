using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TrainStations.Entities;

namespace TrainStations.UnitTests
{
    [TestClass]
    public class CustomerSummariesTests
    {
        [TestMethod]
        public void CustomerSummariesShouldHaveData()
        {
            //Create new Trip
            int customerId = 1;
            decimal totalCost = 2.4m;
            string stationStart = "A";
            string stationEnd = "D";
            int startedJourneyAt = 1;
            decimal cost = 2.4m;
            int zoneFrom = 1;
            int zoneTo = 2;
            CustomerSummaries customerSummary = new CustomerSummaries(customerId, totalCost,
                new List<Trip>() { new Trip(stationStart, stationEnd, startedJourneyAt, cost, zoneFrom, zoneTo) });

            //New customerSummary created
            Assert.IsNotNull(customerSummary);

            //Check that the customerSummary has the right customerId
            Assert.AreEqual(customerId, customerSummary.CustomerId);

            //Check that the customerSummary has the right totalCost
            Assert.AreEqual(totalCost, customerSummary.TotalCost);

            var trip = customerSummary.Trips.FirstOrDefault();
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
