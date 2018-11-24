using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Extensions;
using Orlen.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orlen.Services.RouteService
{
    public class RouteService : BaseService<RouteService>, IRouteService
    {
        public RouteService(DataContext db, ILogger<RouteService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> GetRoute(int fromPointId, int toPointId)
        {

            var result = new List<object>
            {
                new {
                    lat  = 11.2M,
                    lon =3.23M
                },
                new {
                    lat  = 15.1123M,
                    lon = 5.4121M
                }
            }.AsJContainer();
            return await Task.FromResult(result);
        }
    }
}
