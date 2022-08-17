using Integral.Domain.Models;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class StudentWebDataService : CommonWebDataService<Student>, IStudentWebDataService
    {
        protected override string ControllerName => "Students";

        public StudentWebDataService(HttpClient client) : base(client)
        {
        }

        public async Task<Student?> CreateStudent(string firstName, string secondName, string? thirdName = null)
        {
            Uri uri = new(ControllerName + $"?firstName={firstName}&secondName={secondName}&thirdName={thirdName}", UriKind.Relative);

            return await SendRequest<Student>(uri, HttpMethod.Post);
        }

        public async Task<Student?> UpdateStudent(int id, string firstName, string secondName, string? thirdName = null)
        {
            Uri uri = new(ControllerName + $"?id={id}&firstName={firstName}&secondName={secondName}&thirdName={thirdName}", UriKind.Relative);

            return await SendRequest<Student>(uri, HttpMethod.Put);
        }
    }
}
