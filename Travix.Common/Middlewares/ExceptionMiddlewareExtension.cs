using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travix.Common.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        /// <summary>
        ///     Handle all exceptions.
        /// </summary>
        /// <param name="app"></param>
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
