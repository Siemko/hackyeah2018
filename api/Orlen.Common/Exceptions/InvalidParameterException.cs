using System;

namespace Orlen.Common.Exceptions
{
    [Serializable]
    public class InvalidParameterException : ServiceException
    {
        public InvalidParameterException(string message)
            : base(message)
        {
        }
    }
}