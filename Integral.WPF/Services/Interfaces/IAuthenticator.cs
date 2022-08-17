using Integral.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IAuthenticator : IDisposable
    {
        HttpClient Client { get; }

        Task<bool> Authenticate(Uri serverAddress, string username, string password, Role role);
    }
}
