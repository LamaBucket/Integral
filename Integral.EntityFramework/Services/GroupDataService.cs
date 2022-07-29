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

                return await context.Groups.Include(x => x.Leader).Include(x => x.Students).Where(x => x.LeaderId == leaderId && x.GroupType == type.Value).ToListAsync();
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
    }
}
