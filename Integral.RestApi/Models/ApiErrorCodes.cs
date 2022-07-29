namespace Integral.RestApi.Models
{
    public static class ApiErrorCodes
    {
        public static readonly int LoginIncorrectPassword = 101;
        public static readonly int LoginIncorrectUsername = 102;
        public static readonly int LoginIncorrectRole = 103;

        public static readonly int UserNotExist = 201;
        public static readonly int UserAlreadyExists = 202;
        public static readonly int UserAlreadyHasRole = 203;
        public static readonly int UserNotHaveRoles = 204;
        public static readonly int UserNotHaveRole = 205;
        public static readonly int UserHasGroups = 206;

        public static readonly int StudentNotExist = 301;

        public static readonly int GroupNotExist = 401;
        public static readonly int GroupAlreadyExists = 402;
        public static readonly int GroupNotHasStudents = 403;

        public static readonly int MeetingNotExist = 501;

    }
}
