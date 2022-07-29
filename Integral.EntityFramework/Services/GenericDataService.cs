using Integral.Domain.Models;
using Integral.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Integral.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        protected readonly IntegralDbContextFactory _contextFactory;

        public GenericDataService(IntegralDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual async Task<T?> Get(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

                return entity;
            }
        }

        public virtual async Task<IEnumerable<T>?> GetAll()
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                List<T> data = await context.Set<T>().ToListAsync();

                if (data.Count == 0)
                    return null;

                return data;
            }
        }

        public async Task<T> Create(T entity)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

                if (entity is null)
                    return false;


                context.Set<T>().Remove(entity);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (IntegralDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);

                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
