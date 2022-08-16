using Integral.Domain.Services;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class CommonWebDataService<T> : ICommonWebDataService<T>
    {
        public HttpClient Client { get; }


        protected virtual string ControllerName => String.Empty;

        public virtual string CreateEndpoint => ControllerName;

        public virtual string DeleteEndpoint => ControllerName;

        public virtual string UpdateEndpoint => ControllerName;

        public virtual string GetEndpoint => ControllerName;

        public virtual string GetAllEndpoint => ControllerName;


        public async Task<bool> Delete(int id)
        {
            
            UriBuilder ub = new();

            ub.Path = CreateEndpoint;
            ub.Query = $"Id={id}";

            return await SendRequest<bool>(ub.Uri, HttpMethod.Delete);
        }

        public async Task<T?> Get(int id)
        {
            UriBuilder ub = new();

            ub.Path = GetEndpoint;
            ub.Query = $"Id={id}";

            return await SendRequest<T>(ub.Uri, HttpMethod.Get);
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            
            UriBuilder ub = new();

            ub.Path = GetAllEndpoint;

            return await SendRequest<List<T>?>(ub.Uri, HttpMethod.Get);
        }


        protected async Task<TResult?> SendRequest<TResult>(Uri uri, HttpMethod method)
        {
            HttpRequestMessage msg = new()
            {
                RequestUri = uri,
                Method = method
            };

            var response = await Client.SendAsync(msg);

            if(!response.IsSuccessStatusCode)
                throw new WebRequestException(response.Content.ToString(), response.StatusCode);

            TResult? result = JsonConvert.DeserializeObject<TResult>(response.Content.ToString() ?? String.Empty);

            return result;
        }

        public CommonWebDataService(HttpClient client)
        {
            Client = client;
        }
    }
}
