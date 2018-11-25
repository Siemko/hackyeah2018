namespace Orlen.Core.Entities
{
    public class RoutePoints
    {
        public int RouteId { get; set; }
        public int PointId { get; set; }
        public int Order { get; set; }
        public Route Route { get; set; }
        public Point Point { get; set; }
    }
}
