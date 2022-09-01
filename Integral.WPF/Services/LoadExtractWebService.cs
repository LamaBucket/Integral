using Integral.Domain.Models;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class LoadExtractWebService<T> : ILoadExtractWebService<T> where T : DomainObject
    {
        public LoadExtractWebService(IIntegralHttpClientFactory clientFactory, Uri uri)
        {
            ClientFactory = clientFactory;
            RelativeUri = uri;
        }

        public IIntegralHttpClientFactory ClientFactory { get; }


        public Uri RelativeUri { get; set; }


        protected async Task<TResult?> SendRequest<TResult>(Uri uri, HttpMethod method, object? requestBody = null)
        {
            JsonContent? content = null;

            if (requestBody is not null)
            {
                content = JsonContent.Create(requestBody, requestBody.GetType());
            }

            HttpRequestMessage msg = new()
            {
                RequestUri = uri,
                Method = method,
                Content = content,
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

            
            string responseContent = await response.Content.ReadAsStringAsync();

            result = JsonConvert.DeserializeObject<TResult>(responseContent);
            

            msg.Dispose();
            content?.Dispose();

            return result;
        }

        public async Task<IEnumerable<T>?> Extract()
        {  
            return await SendRequest<IEnumerable<T>?>(RelativeUri, HttpMethod.Get);
        }

        public async Task<IEnumerable<T>?> Load(IEnumerable<T> items)
        {
            return await SendRequest<IEnumerable<T>?>(RelativeUri, HttpMethod.Post, items);
        }
    }
}
