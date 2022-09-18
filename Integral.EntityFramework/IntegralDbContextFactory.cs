using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Integral.EntityFramework
{
    public class IntegralDbContextFactory : IDesignTimeDbContextFactory<IntegralDbContext>
    {
        public static string? ConnString;

        public IntegralDbContext CreateDbContext(string[] args = null!)
        {
            if (String.IsNullOrEmpty(ConnString))
                throw new Exception("Connection String Is Null!");

            DbContextOptionsBuilder options = new();            

            options.UseSqlServer(ConnString);

            IntegralDbContext context = new(options.Options);

            return context;
        }
    }
}
