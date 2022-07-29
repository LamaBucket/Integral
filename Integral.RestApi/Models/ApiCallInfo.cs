using Integral.Domain.Models.Enums;
using System.Text;
using System.Text.Json;

namespace Integral.RestApi.Models
{
    public class ApiCallInfo
    {
        public ApiCallInfo(string? username, Role? userRole, string? userIp, string? method, string? endpoint, int statusCode, Exception? exception = null)
        {
            Username = username;
            UserRole = userRole;
            UserIp = userIp;
            Method = method;
            Endpoint = endpoint;
            Exception = exception;
            StatusCode = statusCode;
        }

        public string? Username { get; set; }
        
        public Role? UserRole { get; set; }

        public string? UserIp { get; set; }

        public string? Method { get; set; }

        public string? Endpoint { get; set; }

        public int StatusCode { get; set; }

        public Exception? Exception { get; set; }

        public DateTime UtcTimeOccured { get; private set; } = DateTime.UtcNow;


        public override string ToString()
        {
            return JsonSerializer.Serialize<ApiCallInfo>(this, new JsonSerializerOptions() { });
        }
    }
}
