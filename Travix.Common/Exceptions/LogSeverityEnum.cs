namespace Travix.Common.Exceptions
{
    public enum LogSeverityEnum
    {
        /// <summary>
        ///     Fatal error messages. After a fatal error, the application usually terminates.
        /// </summary>
        FATAL,

        /// <summary>
        ///     Very detailed log messages, potentially of a high frequency and volume
        /// </summary>
        TRACE,

        /// <summary>
        ///     Less detailed and/or less frequent debugging messages
        /// </summary>
        DEBUG,

        /// <summary>
        ///     Warnings which don't appear to the user of the application
        /// </summary>
        WARN,

        /// <summary>
        ///     Error messages
        /// </summary>
        ERROR,

        /// <summary>
        ///     Informational messages
        /// </summary>
        INFO


    }
}
