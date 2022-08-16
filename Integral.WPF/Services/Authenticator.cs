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
        public HttpClient Client { get; private set; } = new();

        public async Task<bool> Authenticate(string serverAddress, string username, string password, Role role)
        {
            Client.BaseAddress = new Uri(serverAddress);

            UriBuilder ub = new();

            ub.Query = $"username={username}&password={password}&role={role}";

            var response = await Client.SendAsync(new(HttpMethod.Post, ub.Uri));

            return true;
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
