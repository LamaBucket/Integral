using Integral.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Integral.EntityFramework
{
    public class IntegralDbContext : DbContext
    {
        public IntegralDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;


        public DbSet<Student> Students { get; set; } = null!;


        public DbSet<Group> Groups { get; set; } = null!;


        public DbSet<Meeting> Meetings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasAlternateKey(a => new { a.Username });


            modelBuilder.Entity<Group>()
                .HasAlternateKey(a => new { a.Name, a.Grade });

            modelBuilder.Entity<UserRole>()
                .HasKey(a => new { a.UserId, a.Role });


            base.OnModelCreating(modelBuilder);
        }
    }
}
