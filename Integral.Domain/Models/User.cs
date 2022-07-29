namespace Integral.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }


        public User(string username, string passwordHash) : base()
        {
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}
