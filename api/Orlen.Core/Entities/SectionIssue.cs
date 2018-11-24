namespace Orlen.Core.Entities
{
    public class SectionIssue
    {
        public int SectionId { get; set; }
        public int IssueId { get; set; }
        public Section Section { get; set; }
        public Issue Issue { get; set; }
    }
}
