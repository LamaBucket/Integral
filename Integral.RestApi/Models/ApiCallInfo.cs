using Integral.Domain.Models.Enums;
using System.Text;

namespace Integral.RestApi.Models
{
    public class ApiCallInfo
    {
        public ApiCallInfo(string? username, Role? userRole, string controllerName, string targetControllerMethod)
        {
            Username = username;
            UserRole = userRole;
            ControllerName = controllerName;
            TargetControllerMethod = targetControllerMethod;
        }

        public string? Username { get; set; }
        
        public Role? UserRole { get; set; }

        public string ControllerName { get; set; }

        public string TargetControllerMethod { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append($"User:{Username},");
            sb.Append($"Role:{UserRole},");
            sb.Append($"Controller:{ControllerName},");
            sb.Append($"ControllerMethod:{TargetControllerMethod};");
            
            return sb.ToString();
        }
    }
}
