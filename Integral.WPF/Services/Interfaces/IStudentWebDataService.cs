using Integral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IStudentWebDataService : ICommonWebDataService<Student>
    {
        Task<Student?> CreateStudent(string firstName, string secondName, string? thirdName = null);

        Task<Student?> UpdateStudent(int id, string firstName, string secondName, string? thirdName = null);
    }
}
