namespace Orlen.Services.PointService.Models
{
    public class AddPointRequest
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsGate { get; set; }
    }
}
