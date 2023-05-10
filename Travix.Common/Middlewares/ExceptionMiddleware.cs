
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sepid.EKYC.Framework.Models;
using System.Net;
using Travix.Common.Exceptions;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Travix.Common.Middlewares
{
    /// <summary>
    ///     Log and wrap all exceptions.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _environment;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleGlobalExceptionAsync(httpContext, exception);
            }
        }

        /// <summary>
        ///     Handle exception.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleGlobalExceptionAsync(HttpContext context, Exception inputException)
        {

            Exception exception = inputException;

            //An unique id to track exceptions in system.
            Guid traceId = Guid.NewGuid();
            LogException(exception, traceId);
            context.Response.StatusCode = (int)GetHttpStatusCode(exception);
            string errorCode = (((exception as TravixException)?.ErrorCode) ?? ErrorCodeEnum.INTERNAL_SERVER_ERROR).ToString();
            ErrorInfo result = new ErrorInfo
            {
                Details = GetDetails(exception),
                ErrorCode = errorCode,
                Message = exception.Message,
                TraceId = traceId.ToString()
            };
            var json = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        /// <summary>
        ///     Log exception details.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="traceId"></param>
        private void LogException(Exception exception, Guid traceId)
        {
            string exceptionMessage = $"TraceId: {traceId}, Processed an unhandled exception of type {exception.GetType().Name}:\r\nMessage: {exception.Message}";
            _logger.LogError(exception, exceptionMessage);
        }

        /// <summary>
        /// Get HttpStatusCode by using exception type.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private HttpStatusCode GetHttpStatusCode(Exception exception)
        {
            if (exception is TravixArgumentException)
                return HttpStatusCode.BadRequest;
            else if (exception is TravixNotFoundException)
                return HttpStatusCode.NotFound;
            else if (exception is TravixException)
                return HttpStatusCode.InternalServerError;
            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        ///     If the app is running in the development environment, all exception details will return as exception details.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetDetails(Exception exception)
        {
            if (!_environment.IsDevelopment())
                return "";

            string details = "";
            Exception tempException = exception;
            while (tempException != null)
            {
                details = tempException.GetType().Name + ": " + tempException.Message;
                if (tempException is TravixException)
                {
                    details += Environment.NewLine + ((TravixException)tempException).TechnicalMessage;
                }
                //Exception StackTrace
                if (!string.IsNullOrEmpty(tempException.StackTrace))
                {
                    details += Environment.NewLine + "Stack Trace: " + tempException.StackTrace + Environment.NewLine + Environment.NewLine;
                }
                tempException = tempException.InnerException;
            }
            return details;
        }
    }
}
