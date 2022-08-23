using Integral.Domain.Models.Enums;

namespace Integral.Domain.Models
{
    public class UserRole
    {
        public int UserId { get; set; }

        public Role Role { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is UserRole role)
                return role.UserId == UserId && role.Role == Role;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId.GetHashCode(), Role.GetHashCode());
        }

        public static bool operator  ==(UserRole left, UserRole right)
        {
            return left.UserId == right.UserId && left.Role == right.Role;
        }

        public static bool operator !=(UserRole left, UserRole right)
        {
            return left.UserId != right.UserId || left.Role != right.Role;
        }
    }
}
