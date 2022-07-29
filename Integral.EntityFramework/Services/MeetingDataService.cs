using Integral.Domain.Models;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework.Services
{
    public class MeetingDataService : GenericDataService<Meeting>, IMeetingDataService
    {
        public MeetingDataService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<Meeting>?> GetAllInGroup(int groupId)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Meetings.Include(m => m.Group).Where(x => x.GroupId == groupId).ToListAsync();
            }
        }

        public async override Task<Meeting?> Get(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Meetings.Include(m => m.Group).FirstOrDefaultAsync(x => x.Id == id);
            }
        }
    }
}
