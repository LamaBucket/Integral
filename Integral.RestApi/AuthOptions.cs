using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Integral.RestApi
{
    public static class AuthOptions
    {
        public static readonly string Issuer = "IntegralWebAppServer";

        public static readonly string Audience = "IntegralWebAppClient";

        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Integr@!A2pHUIHUIHUI"));

        public static readonly TimeSpan DefaultTokenExpirationTime = TimeSpan.FromHours(6);
    }
}
