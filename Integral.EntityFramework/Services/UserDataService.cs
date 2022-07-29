using Integral.Domain.Models;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework.Services
{
    public class UserDataService : GenericDataService<User>, IUserDataService
    {
        public UserDataService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<User?> Get(string username)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                User? entity = await context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(e => e.Username == username);

                return entity;
            }
        }

        public async override Task<User?> Get(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                User? entity = await context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(e => e.Id == id);

                return entity;
            }
        }
    }
}
