using Integral.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IAuthenticator : IDisposable
    {
        IIntegralHttpClientFactory ClientFactory { get; }

        IApplicationStateService ApplicationStateService { get; init; }

        Task<bool> Login(Uri serverAddress, string username, string password, Role role);

        Task<bool> Logout();

    }
}
