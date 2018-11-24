namespace Orlen.Services.SectionService.Models
{
    public class AddSectionRequest
    {
        public int StartPointId { get; set; }
        public int EndPointId { get; set; }
        public string Name { get; set; }
    }
}
