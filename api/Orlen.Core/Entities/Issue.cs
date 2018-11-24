using System.ComponentModel.DataAnnotations.Schema;

namespace Orlen.Core.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Value { get; set; }
        public int IssueTypeId { get; set; }
        public int? PointId { get; set; }
        public int? SectionId { get; set; }
        public Point Point { get; set; }
        public Section Section { get; set; }
        public IssueType IssueType { get; set; }
    }
}
