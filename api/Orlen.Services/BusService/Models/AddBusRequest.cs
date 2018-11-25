using System.Collections.Generic;

namespace Orlen.Services.BusService.Models
{
    public class AddBusRequest
    {
        public string Name { get; set; }
        public int RouteId { get; set; }
        public IEnumerable<int> Stops { get; set; }
    }
}
