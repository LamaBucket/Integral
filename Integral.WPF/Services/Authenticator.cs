using Integral.Domain.Models.Enums;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class Authenticator : IAuthenticator
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public IIntegralHttpClientFactory ClientFactory { get; private set; }

        private object _clientLock = new();

        public bool IsLoggedIn => CurrentRole is not null; 
           

        private Role? _currentRole;

        public Role? CurrentRole
        {
            get => _currentRole;
            set
            {
                _currentRole = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentRole)));
                PropertyChanged?.Invoke(this, new(nameof(IsLoggedIn)));
            }
        }


        public Authenticator(IIntegralHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<bool> Login(Uri serverAddress, string username, string password, Role role)
        {
            await Logout();

            lock (_clientLock)
            {
                ClientFactory.Client.BaseAddress = serverAddress;
            }
            
            string _uri = $"Session?username={username}&password={password}&userRole={role}";

            Uri uri = new(_uri, UriKind.Relative);

            HttpRequestMessage msg = new(HttpMethod.Post, uri);

            var response = await ClientFactory.Client.SendAsync(msg);

            if (!response.IsSuccessStatusCode)
                throw new WebRequestException(await response.Content.ReadAsStringAsync(), response.StatusCode);

            lock (_clientLock)
            {
                if (response.Headers.TryGetValues("set-cookie", out IEnumerable<string>? vals))
                {
                    ClientFactory.Client.DefaultRequestHeaders.Add("Cookie", vals);
                }

                CurrentRole = role;
            }            

            msg.Dispose();

            return true;
        }

        public void Dispose()
        {
            ClientFactory.Dispose();
        }

        public async Task<bool> Logout()
        {
            await Task.Run(() =>
            {
                lock (_clientLock)
                {
                    CurrentRole = null;

                    ClientFactory.ClearCache();
                }
            });
            
            return true;
        }
    }
}
