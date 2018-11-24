using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orlen.Core.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Value { get; set; }
        public List<SectionIssue> SectionIssues { get; set; }
    }
}
