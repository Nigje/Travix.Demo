using System;
namespace Travix.Common.Exceptions
{
    public class TravixArgumentException : TravixException
    {
        public TravixArgumentException(string message = "", ErrorCodeEnum errorCode = ErrorCodeEnum.INVALID_ARGUMENT, string technicalMessage = "") : base(message, errorCode, technicalMessage)
        {
        }

        public TravixArgumentException(string message, Exception innerException, ErrorCodeEnum errorCode = ErrorCodeEnum.INVALID_ARGUMENT, string technicalMessage = "") : base(message, innerException, errorCode, technicalMessage)
        {
        }
    }
}
