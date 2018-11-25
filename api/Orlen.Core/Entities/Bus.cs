using System.Collections.Generic;

namespace Orlen.Core.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public List<BusStop> Stops { get; set; }
    }
}
