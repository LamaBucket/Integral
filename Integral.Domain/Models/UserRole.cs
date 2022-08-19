using Integral.Domain.Models.Enums;

namespace Integral.Domain.Models
{
    public class UserRole
    {
        public int UserId { get; set; }

        public Role Role { get; set; }
    }
}
