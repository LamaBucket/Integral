using Integral.Domain.Models;
using Integral.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.EntityFramework.Services
{
    public abstract class DataManipulationServiceBase<T> : IDataManipulationService<T> where T : DomainObject
    {
        protected readonly IntegralDbContextFactory _contextFactory;

        public DataManipulationServiceBase(IntegralDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public abstract Task<IEnumerable<T>?> Extract(IEnumerable<T> items);

        public abstract Task<DataLoadResult<T>> Load(IEnumerable<T> items);
        
    }
}
