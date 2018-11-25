using System.Collections.Generic;

namespace Orlen.Services.RouteService.Models
{
    public class Node
    {
        public int Id { get; set; }
        public Dictionary<int, List<int>> DistanceDict { get; set; }
        public bool Visited { get; set; }
        public List<Neighbor> Neighbors { get; set; }
    }
}
