using System;

namespace Orlen.Common.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException(string message)
            : base(message)
        {
        }
    }
}