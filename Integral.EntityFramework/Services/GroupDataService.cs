using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework.Services
{
    public class GroupDataService : GenericDataService<Group>, IGroupDataService
    {
        public GroupDataService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<Group>?> GetOwnedGroups(int leaderId, Role role)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                GroupType? type = null;

                switch (role)
                {
                    case Role.Teacher:
                        type = GroupType.ElectiveGroup;
                        break;
                    case Role.ClassPrincipal:
                        type = GroupType.Class;
                        break;
                }

                if (type is null)
                    return null;

                IEnumerable<Group> groups = await context.Groups.Include(x => x.Leader).Include(x => x.Students).Where(x => x.LeaderId == leaderId && x.GroupType == type.Value).ToListAsync();

                return groups.Count() > 0 ? groups : null;
            }
        }

        public async override Task<Group?> Get(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Groups.Include(x => x.Leader).Include(x => x.Students).FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<Group?> Get(string name, int grade)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Groups.Include(x => x.Leader).Include(x => x.Students).FirstOrDefaultAsync(e => e.Name == name && e.Grade == grade);
            }
        }

        public async Task<IEnumerable<User>?> GetUsersThatCanOwnGroup(GroupType type)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                Role requiredRole;

                switch (type)
                {
                    case GroupType.Class:
                        requiredRole = Role.ClassPrincipal;
                        break;
                    case GroupType.ElectiveGroup:
                        requiredRole = Role.Teacher;
                        break;
                    default:
                        return null;
                }

                List<User> users = await context.Users.Include(x => x.UserRoles).Where(x => x.UserRoles != null && x.UserRoles.Any(x => x.Role == requiredRole)).ToListAsync();

                return users.Count > 0 ? users : null;
            }
        }
    }
}
