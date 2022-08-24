using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class IntegralHttpClientFactory : IIntegralHttpClientFactory
    {
        public HttpClient Client { get; private set; }


        public IntegralHttpClientFactory(HttpClient client)
        {
            Client = client;
        }


        public void ClearCache()
        {
            Client = new();
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
