using Integral.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework.Services
{
    public class StudentDataService : GenericDataService<Student>
    {
        public StudentDataService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async override Task<Student?> Get(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                Student? entity = await context.Students.Include(x => x.Groups).FirstOrDefaultAsync(e => e.Id == id);

                return entity;
            }
        }
    }
}
