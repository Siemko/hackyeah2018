using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.BusService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.BusService
{
    public class BusService : BaseService<BusService>, IBusService
    {
        public BusService(DataContext db, ILogger<BusService> logger) : base(db, logger)
        {
        }

        public async Task Add(AddBusRequest request)
        {
            DataContext.Add(new Bus()
            {
                Name = request.Name,
                Stops = request.Stops.ToList()
            });
            await DataContext.SaveChangesAsync();
        }

        public async Task<JContainer> Get()
        {
            return (await DataContext.Buses.Select(b => new
            {
                b.Id,
                b.Name
            }).ToListAsync()).AsJContainer();
        }

        public async Task<JContainer> GetBusStopes(int busId)
        {
            return (await DataContext.BusStops.FirstOrDefaultAsync(v => v.BusId == busId)).AsJContainer();
        }
    }
}
