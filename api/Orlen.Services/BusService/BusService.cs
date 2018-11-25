using System.Collections.Generic;
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
            var bus = new Bus()
            {
                Name = request.Name,
                Stops = new List<BusStop>(),
                RouteId = DataContext.Routes.First().Id
            };

            var stops = new List<BusStop>();
            foreach (var stop in request.Stops)
            {
                bus.Stops.Add(new BusStop
                {
                    Name = stop.Name,
                    PointId = stop.PointId
                });
            }

            await DataContext.AddAsync(bus);

            await DataContext.SaveChangesAsync();
        }

        public async Task<JContainer> Get()
        {
            return (await DataContext.Buses.Select(b => new
            {
                b.Id,
                b.Name,
                b.RouteId
            }).ToListAsync()).AsJContainer();
        }

        public async Task<JContainer> GetBusStopes(int busId)
        {
            var bus = await DataContext.BusStops.Where(v => v.BusId == busId).ToListAsync();
            return bus.AsJContainer();
        }
    }
}
