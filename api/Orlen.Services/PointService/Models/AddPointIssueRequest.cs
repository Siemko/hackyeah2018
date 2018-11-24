namespace Orlen.Services.PointService.Models
{
    public class AddPointIssueRequest
    {
        public int PointId { get; set; }
        public int IssueTypeId { get; set; }
        public decimal? Value { get; set; }
    }
}
