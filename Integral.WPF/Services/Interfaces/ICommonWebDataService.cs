using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface ICommonWebDataService<T>
    {
        Task<IEnumerable<T>?> GetAll();

        Task<T?> Get(int id);

        Task<bool> Delete(int id);
    }
}
