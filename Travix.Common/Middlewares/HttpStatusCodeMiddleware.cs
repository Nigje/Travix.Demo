using Microsoft.AspNetCore.Http;


namespace Travix.Common.Middlewares
{
    public class HttpStatusCodeMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // Call the next middleware in the pipeline
            await _next(context);

            // Check if it's a DELETE request and the response has a 200 OK status code without any reslults.
            if ((context.Request.Method == HttpMethods.Delete || context.Request.Method == HttpMethods.Put)
                && context.Response.StatusCode == StatusCodes.Status200OK
                && context.Response.ContentLength == null)
            {
                // Set the status code to 204 No Content
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                context.Response.ContentLength = 0;
            }

            // Check if it's a DELETE request and the response has a 200 OK status code without any results.
            if (context.Request.Method == HttpMethods.Post
                && context.Response.StatusCode == StatusCodes.Status200OK
                && context.Response.ContentLength == null)
            {
                // Set the status code to 204 No Content
                context.Response.StatusCode = StatusCodes.Status201Created;
                context.Response.ContentLength = 0;
            }
        }
    }
}
