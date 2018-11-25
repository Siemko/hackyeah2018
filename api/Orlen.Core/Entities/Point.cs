using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orlen.Core.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public double Latitude { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public double Longitude { get; set; }
        public bool IsGate { get; set; }
        public List<Issue> Issues { get; set; }
        public List<RoutePoints> RoutePoints { get; set; }
    }
}
