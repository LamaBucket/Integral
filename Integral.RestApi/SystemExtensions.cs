using Integral.Domain.Models.Enums;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Integral.RestApi
{
    public static class SystemExtensions
    {
        public static void Log(this ILogger logger, LogLevel logLevel, ApiCallInfo info)
        {
            logger.Log(logLevel, info.ToString() + Environment.NewLine);
        }
    }
}
