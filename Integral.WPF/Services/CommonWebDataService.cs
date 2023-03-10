using Integral.Domain.Services;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class CommonWebDataService<T> : ICommonWebDataService<T>
    {
        public IIntegralHttpClientFactory ClientFactory { get; }

        protected virtual string ControllerName => String.Empty;

        public virtual string CreateEndpoint => ControllerName;

        public virtual string DeleteEndpoint => ControllerName;

        public virtual string UpdateEndpoint => ControllerName;

        public virtual string GetEndpoint => ControllerName;

        public virtual string GetAllEndpoint => ControllerName;


        public async Task<bool> Delete(int id)
        {
            Uri uri = new(DeleteEndpoint + $"?Id={id}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Delete, false);
        }

        public async Task<T?> Get(int id)
        {
            Uri uri = new(GetEndpoint + $"?Id={id}", UriKind.Relative);

            return await SendRequest<T>(uri, HttpMethod.Get);
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            Uri uri = new(GetAllEndpoint, UriKind.Relative);

            return await SendRequest<List<T>?>(uri, HttpMethod.Get);
        }


        protected async Task<TResult?> SendRequest<TResult>(Uri uri, HttpMethod method, bool HasResponseBody)
        {
            HttpRequestMessage msg = new()
            {
                RequestUri = uri,
                Method = method
            };

            var response = await ClientFactory.Client.SendAsync(msg);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.NoContent:
                    return default(TResult);
                default:
                    throw new WebRequestException(await response.Content.ReadAsStringAsync(), response.StatusCode);
            }

            TResult? result = default(TResult);

            if (HasResponseBody)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TResult>(responseContent);
            }
            
            msg.Dispose();

            return result;
        }

        protected async Task<TResult?> SendRequest<TResult>(Uri uri, HttpMethod method) => await SendRequest<TResult>(uri, method, true);

        public CommonWebDataService(IIntegralHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
    }
}
