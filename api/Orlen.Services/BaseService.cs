using Microsoft.Extensions.Logging;
using Orlen.Core;

namespace Orlen.Services
{
    public class BaseService<T>
    {
        protected readonly ILogger<T> Logger;

        protected readonly DataContext DataContext;

        public BaseService(DataContext db, ILogger<T> logger)
        {
            Logger = logger;
            DataContext = db;
        }      
    }
}
