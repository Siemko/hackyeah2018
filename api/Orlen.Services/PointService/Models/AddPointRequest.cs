namespace Orlen.Services.PointService.Models
{
    public class AddPointRequest
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
