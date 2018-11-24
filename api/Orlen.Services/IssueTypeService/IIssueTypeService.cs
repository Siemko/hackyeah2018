using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.IssueTypeService
{
    public interface IIssueTypeService
    {
        Task<JContainer> Get();
    }
}
