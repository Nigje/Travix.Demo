using System;
using System.Collections.Generic;
using System.Text;

namespace Travix.Common.Exceptions
{
    public class TravixException : Exception
    {
        public TravixException(string message, ErrorCodeEnum errorCode = ErrorCodeEnum.INTERNAL_SERVER_ERROR, string technicalMessage = "")
            : base(message)
        {
            TechnicalMessage = technicalMessage;
            ErrorCode = errorCode;
        }
        public TravixException(string message, Exception innerException, ErrorCodeEnum errorCode = ErrorCodeEnum.INTERNAL_SERVER_ERROR, string technicalMessage = "")
            : base(message,innerException)
        {
            TechnicalMessage = technicalMessage;
            ErrorCode = errorCode;
        }
        /// <summary>
        ///     Error code that indicates a summary of error by using some words or numbers.
        ///     Ex: Its value can be USER_NOT_FOUND when the user is not found in the applicaiton.
        /// </summary>
        public ErrorCodeEnum ErrorCode { get; protected set; }

        /// <summary>
        ///     Technical-details are not allowed to be shown to the user.
        ///     Just log them or use them internally.
        /// </summary>
        public string? TechnicalMessage { get; protected set; }

        /// <summary>
        ///     Severity of the exception. The main usage will be for distinguish logs and monitoring.
        ///     Think about the difference of between severity of a ValidationException and an Exception related to DB connection or Infrastructure. 
        ///     Default: Error.
        /// </summary>
        public LogSeverityEnum Severity { get; protected set; }

        /// <summary>
        ///     A temporal variable that shows whether StackTrace is valuable or not.
        /// </summary>
        public bool LogStackTrace { get; protected set; }
    }
}
