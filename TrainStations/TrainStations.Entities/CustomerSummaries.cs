using System.Collections.Generic;

namespace TrainStations.Entities
{
    public class CustomerSummaries
    {
        public CustomerSummaries()
        { }

        public CustomerSummaries(int customerId, decimal totalCost, List<Trip> trips)
        {
            this.CustomerId = customerId;
            this.TotalCost = totalCost;
            this.Trips = trips;
        }

        public int CustomerId { get; set; }
        public decimal TotalCost { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
