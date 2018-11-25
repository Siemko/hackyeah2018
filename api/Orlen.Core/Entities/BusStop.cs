namespace Orlen.Core.Entities
{
    public class BusStop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        
        public int BusId { get; set; }
        public int PointId { get; set; }
        public Bus Bus { get; set; }
        public Point Point { get; set; }
    }
}
