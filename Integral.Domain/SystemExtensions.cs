using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;

namespace Integral.Domain
{
    public static class SystemExtensions
    {
        public static async Task<bool> HasRole(this IUserDataService dataService, int id, Role role)
        {
            return (await dataService.Get(id))?.UserRoles?.Any(x => x.Role == role) ?? false;
        }

        public static async Task<int> GetId(this IUserDataService dataService, string? username, int defaultValue = -999)
        {
            if (username is null)
                return defaultValue;

            return (await dataService.Get(username))?.Id ?? defaultValue;
        }

        public static async Task<bool> ItemExists(this IGroupDataService dataService, string name, int grade)
        {
            return (await dataService.Get(name, grade)) is not null;
        }

        public static async Task<bool> ItemExists<T>(this IDataService<T> dataService, int id) where T : DomainObject
        {
            return (await dataService.Get(id)) is not null;
        }
    }
}
