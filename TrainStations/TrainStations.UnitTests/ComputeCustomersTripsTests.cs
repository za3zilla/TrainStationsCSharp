using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TrainStations.Entities;

namespace TrainStations.UnitTests
{
    [TestClass]
    public class ComputeCustomersTripsTests
    {
        [TestMethod]
        public void ComputeCustomersTripsShouldHaveData()
        {
            //Create new taps for multiple customers
            int firstCustomerId = 1;
            int secondCustomerId = 2;
            int thirdCustomerId = 3;
            int fourthCustomerId = 4;

            List<Tap> taps = new List<Tap>();
            taps.Add(new Tap(unixTimestamp: 1, customerId: firstCustomerId, station: "A"));
            taps.Add(new Tap(unixTimestamp: 2, customerId: firstCustomerId, station: "D"));

            taps.Add(new Tap(unixTimestamp: 2, customerId: secondCustomerId, station: "B"));
            taps.Add(new Tap(unixTimestamp: 3, customerId: secondCustomerId, station: "C"));

            taps.Add(new Tap(unixTimestamp: 3, customerId: thirdCustomerId, station: "H"));

            taps.Add(new Tap(unixTimestamp: 3, customerId: secondCustomerId, station: "H"));
            taps.Add(new Tap(unixTimestamp: 10, customerId: secondCustomerId, station: "G"));

            taps.Add(new Tap(unixTimestamp: 20, customerId: secondCustomerId, station: "D"));

            taps.Add(new Tap(unixTimestamp: 27, customerId: thirdCustomerId, station: "E"));
            taps.Add(new Tap(unixTimestamp: 30, customerId: thirdCustomerId, station: "E"));
            taps.Add(new Tap(unixTimestamp: 35, customerId: thirdCustomerId, station: "A"));

            taps.Add(new Tap(unixTimestamp: 41, customerId: fourthCustomerId, station: "A"));
            taps.Add(new Tap(unixTimestamp: 47, customerId: fourthCustomerId, station: "I"));

            taps.Add(new Tap(unixTimestamp: 65, customerId: secondCustomerId, station: "F"));

            taps.Add(new Tap(unixTimestamp: 70, customerId: fourthCustomerId, station: "E"));
            taps.Add(new Tap(unixTimestamp: 81, customerId: fourthCustomerId, station: "F"));

            var computedTrips = ComputeCustomersTrips.Compute(taps);

            //Check we have computed our customer trips
            Assert.IsNotNull(computedTrips);
            Assert.AreEqual(4, computedTrips.Count, "summary for each customer");

            //Check first customer total cost
            Assert.AreEqual(2.4m, computedTrips.FirstOrDefault(c => c.CustomerId == firstCustomerId).TotalCost);

            //Check second customer total cost
            Assert.AreEqual(7.2m, computedTrips.FirstOrDefault(c => c.CustomerId == secondCustomerId).TotalCost);

            //Check third customer total cost
            Assert.AreEqual(4.4m, computedTrips.FirstOrDefault(c => c.CustomerId == thirdCustomerId).TotalCost);

            //Check fourth customer total cost
            Assert.AreEqual(5, computedTrips.FirstOrDefault(c => c.CustomerId == fourthCustomerId).TotalCost);
        }
    }
}
