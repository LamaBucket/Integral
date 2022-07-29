using Integral.Domain.Models.Enums;

namespace Integral.Domain.Models
{
    public class UserRole : DomainObject
    {
        public int UserId { get; set; }

        public Role Role { get; set; }
    }
}
