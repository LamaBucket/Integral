using Integral.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public class MeetingDataManipulationService : DataManipulationServiceBase<Meeting>
    {
        public MeetingDataManipulationService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async override Task<IEnumerable<Meeting>?> Extract()
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                List<Meeting> meetings = await context.Meetings.Include(x => x.Group).ToListAsync();

                return meetings.Count > 0 ? meetings : null;
            }
        }

        public async override Task<DataLoadResult<Meeting>> Load(IEnumerable<Meeting> items)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<int> ExistingGroups = await context.Groups.Select(x => x.Id).ToListAsync();

                IEnumerable<Meeting> meetingsToAdd = items.Where(x => ExistingGroups.Contains(x.GroupId));

                foreach (Meeting item in meetingsToAdd)
                {
                    context.Meetings.Add(item);
                }

                await context.SaveChangesAsync();

                IEnumerable<Meeting>? skippedUsers = items.Count() == meetingsToAdd.Count() ? null : items.Except(meetingsToAdd);

                DataLoadResult<Meeting> result = new(skippedUsers);

                return result;
            }
        }
    }
}
