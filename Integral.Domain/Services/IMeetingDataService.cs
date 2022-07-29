using Integral.Domain.Models;

namespace Integral.Domain.Services
{
    public interface IMeetingDataService : IDataService<Meeting>
    {
        Task<IEnumerable<Meeting>?> GetAllInGroup(int groupId);
    }
}
