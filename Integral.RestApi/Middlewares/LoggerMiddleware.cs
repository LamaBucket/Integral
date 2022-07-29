using Integral.Domain.Models.Enums;
using Integral.RestApi.Models;
using System.Security.Claims;

namespace Integral.RestApi.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                await LogHttpRequestAsync(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                await LogExceptionAsync(context, ex);

                await context.Response.WriteAsync("Internal Server Error");
            }
        }

        private async Task LogExceptionAsync(HttpContext context, Exception ex)
        {
            await Task.Run(() =>
            {
                string? clientIp = context.Connection.RemoteIpAddress?.ToString();

                string? username = context.User?.Identity?.Name;
                Role? role = GetUserRole(context);

                string method = context.Request.Method;
                string? endpoint = context.GetEndpoint()?.DisplayName;
                
                int statusCode = context.Response.StatusCode;

                ApiCallInfo info = new(username, role, clientIp, method, endpoint, statusCode, ex);

                _logger.Log(LogLevel.Error, info);
            });
        }

        private async Task LogHttpRequestAsync(HttpContext context)
        {
            await Task.Run(() =>
            {
                string? clientIp = context.Connection.RemoteIpAddress?.ToString();

                string? username = context.User?.Identity?.Name;
                Role? role = GetUserRole(context);

                string method = context.Request.Method;
                string? endpoint = context.GetEndpoint()?.DisplayName;
                
                int statusCode = context.Response.StatusCode;

                ApiCallInfo info = new(username, role, clientIp, method, endpoint, statusCode);

                _logger.Log(LogLevel.Information, info);
            });
        }

        private static Role? GetUserRole(HttpContext context)
        {
            string? value = context.User.FindFirstValue(ClaimTypes.Role);

            bool ok = Enum.TryParse<Role>(value, out Role role);

            if (!ok)
                return null;
            else
                return role;
        }
    }
}
