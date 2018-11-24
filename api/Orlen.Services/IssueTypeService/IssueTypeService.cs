using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Extensions;
using Orlen.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.IssueTypeService
{
    public class IssueTypeService : BaseService<IssueTypeService>, IIssueTypeService
    {
        public IssueTypeService(DataContext db, ILogger<IssueTypeService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> Get()
        {
            return (await DataContext.IssueTypes.Select(i =>
            new
            {
                i.Id,
                i.Name,
            }).ToListAsync())
            .AsJContainer();
        }
    }
}
