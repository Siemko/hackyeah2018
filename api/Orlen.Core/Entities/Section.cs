using System.Collections.Generic;

namespace Orlen.Core.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartId { get; set; }
        public int EndId { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public List<SectionIssue> SectionIssues { get; set; }
    }
}
