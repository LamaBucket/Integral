using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IIntegralHttpClientFactory : IDisposable
    {
        HttpClient Client { get; }

        void ClearCache();
    }
}
