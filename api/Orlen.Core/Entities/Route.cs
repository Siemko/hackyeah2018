using System.Collections.Generic;

namespace Orlen.Core.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public List<RoutePoint> RoutePoints { get; set; }
    }
}
