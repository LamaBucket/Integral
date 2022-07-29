using Integral.RestApi.Models;
using System.Text;

namespace Integral.RestApi
{
    public static class SystemExtensions
    {
        public static void LogError(this ILogger logger, ApiCallInfo info, Exception ex)
        {
            logger.LogError(ex, message: info.ToString());
        }

        public static void LogInformation(this ILogger logger, ApiCallInfo info)
        {
            logger.LogInformation(info.ToString());
        }
    }
}
