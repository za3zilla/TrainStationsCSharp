using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrainStations.Entities;

namespace TrainStations
{
    public static class ComputeCustomersTrips
    {
        public static void ComputeTrips(string inputPath, string outputPath)
        {
            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
                throw new ArgumentNullException("Please your input or output path");

            //Read from json file
            var inputJsonString = File.ReadAllText(inputPath);
            List<Tap> taps = JsonSerializer.Deserialize<List<Tap>>(inputJsonString);

            //Compute Trips
            var trips = Compute(taps);

            //Write to json file
            string outputJsonString = JsonSerializer.Serialize(trips, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputPath, outputJsonString);
        }

        public static List<CustomerSummaries> Compute(List<Tap> taps)
        {
            Dictionary<string, List<int>> stationsDic = GetStationsZonesDic();

            Dictionary<(int, int), decimal> princingDic = GetZonesCostDic();

            var orderdTaps = taps.OrderBy(i => i.CustomerId).ThenBy(i => i.UnixTimestamp).ToList();
            List<Trip> trips = new List<Trip>();
            Dictionary<int, CustomerSummaries> customerSummariesDic = new Dictionary<int, CustomerSummaries>();
            for (int i = 0; i < orderdTaps.Count(); i += 2)
            {
                CustomerSummaries CustomerSummary = new CustomerSummaries();
                if (customerSummariesDic.TryGetValue(orderdTaps[i].CustomerId, out CustomerSummary))
                {
                    decimal? minCost;
                    CustomerSummary.Trips.Add(GetTrip(stationsDic, princingDic, orderdTaps, i, out minCost));
                    CustomerSummary.TotalCost += minCost.Value;
                }
                else
                {
                    decimal? minCost;
                    var trip = GetTrip(stationsDic, princingDic, orderdTaps, i, out minCost);
                    var customerId = orderdTaps[i].CustomerId;
                    CustomerSummary = new CustomerSummaries(customerId, minCost.Value, new List<Trip>() { trip });
                    customerSummariesDic.Add(customerId, CustomerSummary);
                }
            }

            return customerSummariesDic.Values.ToList();
        }

        private static Dictionary<(int, int), decimal> GetZonesCostDic()
        {
            var princingDic = new Dictionary<(int, int), decimal>();
            princingDic.Add((1, 1), 2.4m);
            princingDic.Add((2, 2), 2.4m);
            princingDic.Add((3, 3), 2);
            princingDic.Add((4, 4), 2);
            princingDic.Add((1, 2), 2.4m);
            princingDic.Add((2, 1), 2.4m);
            princingDic.Add((3, 4), 2);
            princingDic.Add((4, 3), 2);
            princingDic.Add((3, 1), 2.8m);
            princingDic.Add((3, 2), 2.8m);
            princingDic.Add((4, 1), 3);
            princingDic.Add((4, 2), 3);
            princingDic.Add((1, 3), 2.8m);
            princingDic.Add((2, 3), 2.8m);
            princingDic.Add((1, 4), 3);
            princingDic.Add((2, 4), 3);
            return princingDic;
        }

        private static Dictionary<string, List<int>> GetStationsZonesDic()
        {
            var stationsDic = new Dictionary<string, List<int>>();
            stationsDic.Add("A", new List<int>() { 1 });
            stationsDic.Add("B", new List<int>() { 1 });
            stationsDic.Add("C", new List<int>() { 2, 3 });
            stationsDic.Add("D", new List<int>() { 2 });
            stationsDic.Add("E", new List<int>() { 2, 3 });
            stationsDic.Add("F", new List<int>() { 3, 4 });
            stationsDic.Add("G", new List<int>() { 4 });
            stationsDic.Add("H", new List<int>() { 4 });
            stationsDic.Add("I", new List<int>() { 4 });
            return stationsDic;
        }

        private static Trip GetTrip(Dictionary<string, List<int>> stationsDic, Dictionary<(int, int), decimal> princingDic, List<Tap> orderdTaps, int i, out decimal? minCost)
        {
            var stationStart = orderdTaps[i].Station;
            var stationEnd = orderdTaps[i + 1].Station;
            var zoneFromList = stationsDic.GetValueOrDefault(stationStart);
            var zoneToList = stationsDic.GetValueOrDefault(stationEnd);
            minCost = null;
            int zoneFrom = 0;
            int zoneTo = 0;
            foreach (var from in zoneFromList)
            {
                foreach (var to in zoneToList)
                {
                    var cost = princingDic.GetValueOrDefault((from, to));
                    if (cost < minCost || minCost == null)
                    {
                        minCost = cost;
                        zoneFrom = from;
                        zoneTo = to;
                    }
                }
            }

            return new Trip(stationStart, stationEnd, orderdTaps[i].UnixTimestamp, minCost.Value, zoneFrom, zoneTo);
        }
    }
}
