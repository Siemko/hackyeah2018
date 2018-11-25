namespace Orlen.Core.Entities
{
    public class RoutePoint
    {
        public int RouteId { get; set; }
        public int PointId { get; set; }
        public Route Route { get; set; }
        public Point Point { get; set; }
    }
}
