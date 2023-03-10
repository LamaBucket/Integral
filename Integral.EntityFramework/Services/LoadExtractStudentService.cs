using Integral.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public class LoadExtractStudentService : LoadExtractServiceBase<Student>
    {
        public LoadExtractStudentService(IntegralDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async override Task<IEnumerable<Student>?> Extract()
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                List<Student> students = await context.Students.ToListAsync();

                return students.Count > 0 ? students : null;
            }
        }

        public async override Task<IEnumerable<Student>?> Load(IEnumerable<Student> items)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                foreach (Student item in items)
                {
                    context.Students.Add(item);
                }

                await context.SaveChangesAsync();
                
                return null;
            }
        }
    }
}
