using Integral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.Domain.Services
{
    public interface ILoadExtractService<T> where T : DomainObject
    {
        Task<IEnumerable<T>?> Load(IEnumerable<T> items);

        Task<IEnumerable<T>?> Extract();
    }
}
