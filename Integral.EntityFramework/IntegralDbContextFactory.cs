using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Integral.EntityFramework
{
    public class IntegralDbContextFactory : IDesignTimeDbContextFactory<IntegralDbContext>
    {
        public const string ConnString = "Data Source=localhost;Initial Catalog=Integral_App4;Integrated Security=True";

        public IntegralDbContext CreateDbContext(string[] args = null!)
        {
            DbContextOptionsBuilder options = new();

            options.UseSqlServer(ConnString);

            IntegralDbContext context = new(options.Options);

            return context;
        }
    }
}
