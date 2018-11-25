using System.Collections.Generic;
using Orlen.Core.Entities;

namespace Orlen.Services.BusService.Models
{
    public class AddBusRequest
    {
        public string Name { get; set; }
        public IEnumerable<BusStop> Stops { get; set; }
    }
}
