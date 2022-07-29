using Integral.Domain.Models.Enums;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Integral.RestApi
{
    public static class SystemExtensions
    {
        public static void LogError(this ILogger logger, ApiCallInfo info, Exception ex)
        {
            StringBuilder sb = new();

            sb.AppendLine(info.ToString());
            sb.AppendLine($"Error:{ex.Message},");
            sb.AppendLine($"StackTrace:{ex.StackTrace};");

            sb.AppendLine();

            logger.LogError(message: sb.ToString());
        }

        public static void LogInformation(this ILogger logger, ApiCallInfo info)
        {
            logger.LogInformation(info.ToString());
        }

        public static Role? GetUserRole(this Controller controller)
        {
            string? value = controller.User.FindFirstValue(ClaimTypes.Role);

            bool ok = Enum.TryParse<Role>(value, out Role role);

            if (!ok)
                return null;
            else
                return role;
        }
    }
}
