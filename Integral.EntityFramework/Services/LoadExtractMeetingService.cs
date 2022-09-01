using Integral.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public class LoadExtractMeetingService : LoadExtractServiceBase<Meeting>
    {
        public LoadExtractMeetingService(IntegralDbContextFactory contextFactory) : base(contextFactory)
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

        public async override Task<IEnumerable<Meeting>?> Load(IEnumerable<Meeting> items)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Tuple<int, string>> ExistingGroups = await context.Groups.Select(x => Tuple.Create(x.Grade, x.Name)).ToListAsync();

                IEnumerable<Meeting> meetingsToAdd = items.Where(x => ExistingGroups.Any(y => y.Item1 == x.Group?.Grade && y.Item2 == x.Group?.Name));

                foreach (Meeting item in meetingsToAdd)
                {
                    context.Meetings.Add(item);
                }

                await context.SaveChangesAsync();

                IEnumerable<Meeting>? skippedMeetings = items.Count() == meetingsToAdd.Count() ? null : items.Except(meetingsToAdd);

                return skippedMeetings;
            }
        }
    }
}
