using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Hz.Infrastructure.Middleware;

namespace Hz.Infrastructure
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}