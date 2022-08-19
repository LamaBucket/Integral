using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework.Services
{
    public class UserDataService : GenericDataService<User>, IUserDataService
    {
        public UserDataService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<User?> AssignRole(int userId, Role role)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                User? entity = await context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(e => e.Id == userId);


                if (entity is not null)
                {
                    if (entity.UserRoles is null)
                    {
                        entity.UserRoles = new List<UserRole>() { new() { Role = role, UserId = userId } };
                    }
                    else
                    {
                        entity.UserRoles.Add(new() { Role = role, UserId = userId });
                    }

                    await context.SaveChangesAsync();
                    
                }

                return entity;
            }
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

        public async Task<User?> UnassignRole(int userId, Role role)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                User? entity = await context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(e => e.Id == userId);

                if (entity is not null && entity.UserRoles is not null)
                {
                    UserRole? _role = entity.UserRoles.FirstOrDefault(x => x.Role == role);

                    if (_role is not null)
                    {
                        entity.UserRoles.Remove(_role);

                        await context.SaveChangesAsync();
                        
                    }
                }

                return entity;
            }
        }
    }
}
