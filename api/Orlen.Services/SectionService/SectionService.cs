using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.SectionService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.SectionService
{
    public class SectionService : BaseService<SectionService>, ISectionService
    {
        public SectionService(DataContext db, ILogger<SectionService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> GetAll()
        {
            return (await DataContext.Sections.Select(s =>
            new
            {
                s.Id,
                Start = new { s.Start.Lat, s.Start.Lon },
                End = new { s.End.Lat, s.End.Lon },
                s.Name,
                Issues = s.SectionIssues.Select(si => si.Issue).Select(i =>
                new
                {
                    i.Name,
                    i.Value
                })
            }).ToListAsync())
            .AsJContainer();
        }

        public async Task Add(AddSectionRequest request)
        {
            if (!await DataContext.Points.AnyAsync(p => p.Id == request.StartPointId))
                throw new ResourceNotFoundException($"There is no point with id {request.StartPointId}");

            if (!await DataContext.Points.AnyAsync(p => p.Id == request.EndPointId))
                throw new ResourceNotFoundException($"There is no point with id {request.EndPointId}");

            DataContext.Sections.Add(new Section()
            {
                EndId = request.EndPointId,
                StartId = request.StartPointId,
                Name = request.Name
            });

            await DataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var section = await DataContext.Sections.FirstOrDefaultAsync(s => s.Id == id);
            if (section == null)
                throw new ResourceNotFoundException($"There is section with id {id}");

            DataContext.Remove(section);

            await DataContext.SaveChangesAsync();
        }
    }
}
