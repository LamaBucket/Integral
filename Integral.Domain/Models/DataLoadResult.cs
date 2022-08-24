using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.Domain.Models
{
    public class DataLoadResult<T> where T : DomainObject
    {
        public bool LoadedAllItems => NotLoadedItems is null;

        public IEnumerable<T>? NotLoadedItems { get; init; }

        public DataLoadResult(IEnumerable<T>? notLoadedItems)
        {
            NotLoadedItems = notLoadedItems;
        }
    }
}
