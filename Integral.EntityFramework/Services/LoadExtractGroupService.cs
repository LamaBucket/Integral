using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public class LoadExtractGroupService : LoadExtractServiceBase<Group>
    {
        public LoadExtractGroupService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async override Task<IEnumerable<Group>?> Extract()
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                List<Group> groups = await context.Groups.Include(x => x.Leader).ToListAsync();

                return groups.Count > 0 ? groups : null;
            }
        }

        public async override Task<IEnumerable<Group>?> Load(IEnumerable<Group> items)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                Dictionary<GroupType, IEnumerable<int>> potentialLeaders = new();

                potentialLeaders.Add(GroupType.ElectiveGroup, context.Users.Include(x => x.UserRoles).Where(x => x.UserRoles != null && x.UserRoles.Any(y => y.Role == Role.Teacher)).Select(x => x.Id));
                potentialLeaders.Add(GroupType.Class, context.Users.Include(x => x.UserRoles).Where(x => x.UserRoles != null && x.UserRoles.Any(y => y.Role == Role.ClassPrincipal)).Select(x => x.Id));

                IEnumerable<Tuple<string, int>> ExistingGroups = await context.Groups.Select(x => new Tuple<string, int>(x.Name, x.Grade)).ToListAsync();

                IEnumerable<Group> groupsToAdd = items.Where(x => !ExistingGroups.Any(y => y.Item1 == x.Name && y.Item2 == x.Grade));

                groupsToAdd = groupsToAdd.Where(x => potentialLeaders[x.GroupType].Contains(x.LeaderId));

                foreach(Group item in groupsToAdd)
                {
                    context.Groups.Add(item);
                }

                await context.SaveChangesAsync();

                IEnumerable<Group>? skippedGroups = items.Count() == groupsToAdd.Count() ? null : items.Except(groupsToAdd);

                return skippedGroups;
            }
        }
    }
}
