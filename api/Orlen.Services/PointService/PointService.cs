using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.PointService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.PointService
{
    public class PointService : BaseService<PointService>, IPointService
    {
        public PointService(DataContext db, ILogger<PointService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> GetAll()
        {
            return (await DataContext.Points.Select(p => new { p.Id, p.Lat, p.Lon, p.Name }).ToListAsync()).AsJContainer();
        }
        public async Task Add(AddPointRequest request)
        {
            DataContext.Points.Add(new Point
            {
                Lat = request.Lat,
                Lon = request.Lon,
                Name = request.Name
            });
            await DataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var point = await DataContext.Points.FirstOrDefaultAsync(p => p.Id == id);
            if (point == null)
                throw new ResourceNotFoundException($"There is no point with id {id}");
            DataContext.Remove(point);

            await DataContext.SaveChangesAsync();
        }

        public async Task Update(UpdatePointRequest request)
        {
            var point = await DataContext.Points.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (point == null)
                throw new ResourceNotFoundException($"There is no point with id {request.Id}");

            point.Lat = request.Lat;
            point.Lon = request.Lon;
            point.Name = request.Name;
            await DataContext.SaveChangesAsync();
        }
    }
}
