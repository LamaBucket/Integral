using Integral.Domain.Models.Enums;
using Integral.WPF.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class Authenticator : IAuthenticator
    {
        public HttpClient Client { get; private set; }

        public Authenticator(HttpClient client)
        {
            Client = client;
        }

        public async Task<bool> Authenticate(Uri serverAddress, string username, string password, Role role)
        {
            Client.BaseAddress = serverAddress;

            string _uri = $"Session?username={username}&password={password}&role={role}";

            Uri uri = new(_uri, UriKind.Relative);

            var response = await Client.SendAsync(new(HttpMethod.Post, uri));

            if(response.Headers.TryGetValues("set-cookie", out IEnumerable<string>? vals))
            {
                Client.DefaultRequestHeaders.Add("Cookie", vals);
            }

            return true;
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
