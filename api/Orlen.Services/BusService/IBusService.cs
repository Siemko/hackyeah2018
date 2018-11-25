using Newtonsoft.Json.Linq;
using Orlen.Services.BusService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.BusService
{
    public interface IBusService
    {
        Task<JContainer> Get();
        Task Add(AddBusRequest request);
        Task<JContainer> GetBusStopes(int busId);
    }
}
