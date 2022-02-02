namespace TrainStations.Entities
{
    public class Trip
    {
        public Trip(string stationStart, string stationEnd, int startedJourneyAt, decimal cost, int zoneFrom, int zoneTo)
        {
            this.StationStart = stationStart;
            this.StationEnd = stationEnd;
            this.StartedJourneyAt = startedJourneyAt;
            this.Cost = cost;
            this.ZoneFrom = zoneFrom;
            this.ZoneTo = zoneTo;
        }

        public string StationStart { get; set; }
        public string StationEnd { get; set; }
        public int StartedJourneyAt { get; set; }
        public decimal Cost { get; set; }
        public int ZoneFrom { get; set; }
        public int ZoneTo { get; set; }
    }
}
