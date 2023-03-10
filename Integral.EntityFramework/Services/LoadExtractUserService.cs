using Integral.Domain.Models;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public class LoadExtractUserService : LoadExtractServiceBase<User>
    {
        public LoadExtractUserService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async override Task<IEnumerable<User>?> Extract()
        {
            using(IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                List<User> users = await context.Users.Include(x => x.UserRoles).ToListAsync();

                return users.Count > 0 ? users : null;
            }
        }

        public async override Task<IEnumerable<User>?> Load(IEnumerable<User> items)
        {
            using(IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<string> ExistingUsernames = await context.Users.Select(x => x.Username).ToListAsync();

                IEnumerable<User> usersToAdd = items.Where(x => !ExistingUsernames.Contains(x.Username));

                foreach(User item in usersToAdd)
                {
                    context.Users.Add(item);
                }

                await context.SaveChangesAsync();

                IEnumerable<User>? skippedUsers = items.Count() == usersToAdd.Count() ? null : items.Except(usersToAdd);

                return skippedUsers;
            }
        }
    }
}
