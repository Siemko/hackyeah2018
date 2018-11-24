using System;

namespace Orlen.Common.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : ServiceException
    {
        public ResourceNotFoundException(string message)
            : base(message)
        {
        }
    }
}