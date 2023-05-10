using System;

namespace Travix.Common.Exceptions
{
    /// <summary>
    ///     Use this type of exception when a value is not found.
    /// </summary>
    public class TravixNotFoundException : TravixException
    {
        public TravixNotFoundException(string message = "", ErrorCodeEnum errorCode = ErrorCodeEnum.NOT_FOUND, string technicalMessage = "") : base(message, errorCode, technicalMessage)
        {
        }

        public TravixNotFoundException(string message , Exception innerException, ErrorCodeEnum errorCode = ErrorCodeEnum.NOT_FOUND, string technicalMessage = "") : base(message, innerException, errorCode, technicalMessage)
        {
        }
    }
}
