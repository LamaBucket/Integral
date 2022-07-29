using Integral.RestApi.Models;
using System.Text;

namespace Integral.RestApi
{
    public static class SystemExtensions
    {
        public static string FormatToFileLog(this ApiCallState state, Exception? exception)
        {
            StringBuilder sb = new();

            sb.Append($"User:{state.Username},");
            sb.Append($"Role:{state.UserRole},");
            sb.Append($"Controller:{state.ApiControllerType.Name},");
            sb.Append($"ControllerMethod:{state.ApiControllerMethodName},");
            sb.Append($"Success:{state.Success}");

            if (!state.Success)
            {
                if (exception is not null)
                {
                    sb.Append($", DueTo:{exception.Message}.");
                }
                else
                {
                    sb.Append(", DueTo:UnknownError");
                }
               
            }

            sb.AppendLine();

            return sb.ToString();
        }
    }
}
