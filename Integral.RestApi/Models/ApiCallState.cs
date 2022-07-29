using Integral.Domain.Models.Enums;

namespace Integral.RestApi.Models
{
    public class ApiCallState
    {
        public ApiCallState(string username, Role userRole, string apiControllerMethodName, Type apiControllerType, bool success)
        {
            Username = username;
            UserRole = userRole;
            ApiControllerMethodName = apiControllerMethodName;
            ApiControllerType = apiControllerType;
            Success = success;
        }

        public string Username { get; set; }

        public Role UserRole { get; set; }


        public string ApiControllerMethodName { get; set; }

        public Type ApiControllerType { get; set; }


        public bool Success { get; set; }

    }
}
