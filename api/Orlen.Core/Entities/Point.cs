using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orlen.Core.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public double Lat { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public double Lon { get; set; }
        public bool IsGate { get; set; }
        public List<Issue> Issues { get; set; }
        public List<RoutePoint> RoutePoints { get; set; }
    }
}
