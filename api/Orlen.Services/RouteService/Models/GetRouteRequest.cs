namespace Orlen.Services.RouteService.Models
{
    public class GetRouteRequest
    {
        public int StartPointId { get; set; }
        public int EndPointId { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
    }
}
