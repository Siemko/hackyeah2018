using System;

namespace Orlen.Common.Exceptions
{
    [Serializable]
    public class ResourceDuplicatedException : ServiceException
    {
        public ResourceDuplicatedException(string message)
            : base(message)
        {
        }
    }
}