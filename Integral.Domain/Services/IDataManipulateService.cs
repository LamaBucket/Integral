using Integral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.Domain.Services
{
    public interface IDataManipulateService<T> where T : DomainObject
    {
        Task<DataLoadResult<T>> Load(IEnumerable<T> items);

        Task<IEnumerable<T>?> Extract(IEnumerable<T> items);
    }
}
