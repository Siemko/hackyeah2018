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
            var sections = await DataContext.Sections.Select(s =>
            new
            {
                s.Id,
                s.StartId,
                s.EndId,
                s.Name,
                Issues = s.Issues.Select(i => new
                {
                    i.IssueType.Name,
                    i.Value,
                    i.IssueTypeId
                })
            }).ToListAsync();

            return sections.AsJContainer();
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

        public async Task AddIssue(AddSectionIssueRequest request)
        {
            if (await DataContext.Issues.AnyAsync(i => i.IssueTypeId == request.IssueTypeId && i.SectionId == request.SectionId))
                return;

            var section = await DataContext.Sections.FirstOrDefaultAsync(p => p.Id == request.SectionId);
            if (section == null)
                throw new ResourceNotFoundException($"There is no section with id {request.SectionId}");

            var issueType = await DataContext.IssueTypes.FirstOrDefaultAsync(i => i.Id == request.IssueTypeId);
            if (issueType == null)
                throw new ResourceNotFoundException($"There is no issue type with id {request.IssueTypeId}");

            var oppositeSection = await DataContext.Sections.FirstOrDefaultAsync(s => s.StartId == section.EndId && s.EndId == section.StartId);
            DataContext.Issues.Add(new Issue()
            {
                IssueTypeId = request.IssueTypeId,
                SectionId = request.SectionId,
                Value = request.Value,
            });

            if (oppositeSection != null)
                DataContext.Issues.Add(new Issue()
                {
                    IssueTypeId = request.IssueTypeId,
                    SectionId = oppositeSection.Id,
                    Value = request.Value,
                });


            await DataContext.SaveChangesAsync();
        }

        public async Task ClearIssues(int sectionId)
        {
            var section = await DataContext.Sections.FirstOrDefaultAsync(p => p.Id == sectionId);
            if (section == null)
                throw new ResourceNotFoundException($"There is no section with id {sectionId}");

            var oppositeSection = await DataContext.Sections.FirstOrDefaultAsync(s => s.StartId == section.EndId && s.EndId == section.StartId);

            var issues = await DataContext.Issues.Where(i => i.SectionId == section.Id || i.SectionId == oppositeSection.Id).ToListAsync();

            foreach (var issue in issues)
                DataContext.Remove(issue);

            await DataContext.SaveChangesAsync();
        }
    }
}
