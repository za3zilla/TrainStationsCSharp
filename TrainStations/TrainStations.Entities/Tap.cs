namespace TrainStations.Entities
{
    public class Tap
    {
        public Tap(int unixTimestamp, int customerId, string station)
        {
            this.UnixTimestamp = unixTimestamp;
            this.CustomerId = customerId;
            this.Station = station;
        }

        public int UnixTimestamp { get; set; }
        public int CustomerId { get; set; }
        public string Station { get; set; }
    }
}
