using Newtonsoft.Json.Linq;
using Orlen.Services.PointService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.PointService
{
    public interface IPointService
    {
        Task<JContainer> GetAll();
        Task Add(AddPointRequest request);
        Task Delete(int id);
        Task Update(UpdatePointRequest request);
    }
}
