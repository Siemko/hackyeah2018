using Newtonsoft.Json.Linq;
using Orlen.Services.SectionService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.SectionService
{
    public interface ISectionService
    {
        Task<JContainer> GetAll();
        Task Add(AddSectionRequest request);
        Task Delete(int id);
    }
}
