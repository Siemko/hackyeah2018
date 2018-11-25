namespace Orlen.Services.RouteService.Models
{
    public class GenerateRouteRequest
    {
        public string StartPointName { get; set; }
        public string EndPointName { get; set; }
        public int StartPointId { get; set; }
        public int EndPointId { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
    }
}
