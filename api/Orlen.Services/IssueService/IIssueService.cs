using Newtonsoft.Json.Linq;
using Orlen.Services.IssueService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.IssueService
{
    public interface IIssueService
    {
        Task<JContainer> GetAll();
        Task Add(AddIssueRequest request);
        Task Update(UpdateIssueRequest request);
        Task Delete(int id);
    }
}
