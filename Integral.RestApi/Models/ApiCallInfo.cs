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


        protected DateTime _timeOccured { get; set; } = DateTime.UtcNow;


        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Time:{_timeOccured.ToString("dd-MM-yyyy/hh-mm-ss")}");
            sb.AppendLine($"User:{Username},");
            sb.AppendLine($"Role:{UserRole},");
            sb.AppendLine($"Controller:{ControllerName},");
            sb.AppendLine($"ControllerMethod:{TargetControllerMethod};");
            
            return sb.ToString();
        }
    }
}
