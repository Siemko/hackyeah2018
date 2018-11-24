using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.IssueService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.IssueService
{
    public class IssueService : BaseService<IssueService>, IIssueService
    {
        public IssueService(DataContext db, ILogger<IssueService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> GetAll()
        {
            return (await DataContext.Issues.Select(i =>
            new
            {
                i.Id,
                i.Name,
                i.Value
            }).ToListAsync())
            .AsJContainer();
        }

        public async Task Add(AddIssueRequest request)
        {
            DataContext.Issues.Add(new Issue()
            {
                Name = request.Name,
                Value = request.Value
            });

            await DataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var issue = await DataContext.Issues.FirstOrDefaultAsync(s => s.Id == id);
            if (issue == null)
                throw new ResourceNotFoundException($"There is issue with id {id}");

            foreach (var sectionIssue in await DataContext.SectionIssue.Where(s => s.IssueId == id).ToListAsync())
                DataContext.Remove(sectionIssue);

            await DataContext.SaveChangesAsync();
            DataContext.Issues.Remove(issue);
            await DataContext.SaveChangesAsync();
        }

        public async Task Update(UpdateIssueRequest request)
        {
            var issue = await DataContext.Issues.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (issue == null)
                throw new ResourceNotFoundException($"There is issue with id {request.Id}");

            issue.Name = request.Name;
            issue.Value = request.Value;

            await DataContext.SaveChangesAsync();
        }
    }
}
