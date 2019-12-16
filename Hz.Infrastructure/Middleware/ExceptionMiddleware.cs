using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hz.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Logger.ILogger _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            Logger.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                var returnData = "{\"code\":1001,\"message\":\""+ex.Message+"\"}";
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(returnData);
            }
        }
    }
}
