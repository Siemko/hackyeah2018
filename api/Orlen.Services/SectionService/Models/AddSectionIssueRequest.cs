namespace Orlen.Services.SectionService.Models
{
    public class AddSectionIssueRequest
    {
        public int SectionId { get; set; }
        public int IssueTypeId { get; set; }
        public decimal? Value { get; set; }
    }
}
