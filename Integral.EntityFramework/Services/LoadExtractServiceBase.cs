using Integral.Domain.Models;
using Integral.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public abstract class LoadExtractServiceBase<T> : ILoadExtractService<T> where T : DomainObject
    {
        protected readonly IntegralDbContextFactory _contextFactory;

        public LoadExtractServiceBase(IntegralDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public abstract Task<IEnumerable<T>?> Extract();

        public abstract Task<IEnumerable<T>?> Load(IEnumerable<T> items);
        
    }
}
