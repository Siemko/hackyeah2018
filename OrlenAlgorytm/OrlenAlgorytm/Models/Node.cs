using System.Collections.Generic;

namespace OrlenAlgorytm.Models
{
    public class Node
    {
        public string Name { get; set; }
        public Dictionary<string, List<string>> DistanceDict { get; set; }
        public bool Visited { get; set; }
        public List<Neighbor> Neighbors { get; set; }
    }
}
