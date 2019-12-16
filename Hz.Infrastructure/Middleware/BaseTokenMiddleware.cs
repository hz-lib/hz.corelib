using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hz.Infrastructure.Middleware {
    public class BaseTokenMiddleware {
        private readonly RequestDelegate _next;
        private readonly string _token = "token";

        public BaseTokenMiddleware (RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context) {
            // context.Request.Headers.ContainsKey(_token);
            await _next (context);
        }
    }
}