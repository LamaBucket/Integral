using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DataManagementController : Controller
    {
        public DataManagementController(ILoadExtractService<User> usersLoadExtractService,
                              ILoadExtractService<Meeting> meetingsLoadExtractService,
                              ILoadExtractService<Group> groupsLoadExtractService,
                              ILoadExtractService<Student> studentsLoadExtractService)
        {
            UsersLoadExtractService = usersLoadExtractService;
            MeetingsLoadExtractService = meetingsLoadExtractService;
            GroupsLoadExtractService = groupsLoadExtractService;
            StudentsLoadExtractService = studentsLoadExtractService;
        }

        public ILoadExtractService<User> UsersLoadExtractService { get; set; }

        public ILoadExtractService<Meeting> MeetingsLoadExtractService { get; set; }

        public ILoadExtractService<Group> GroupsLoadExtractService { get; set; }

        public ILoadExtractService<Student> StudentsLoadExtractService { get; set; }



        [HttpGet("Users")]
        [Authorize(Roles = "Admin, Authority")]
        public async Task<ActionResult> ExtractUsers() => await ExtractData<User>(UsersLoadExtractService);


        [HttpGet("Meetings")]
        [Authorize(Roles = "Admin, Authority, ClassPrincipal, Teacher")]
        public async Task<ActionResult> ExtractMeetings() => await ExtractData<Meeting>(MeetingsLoadExtractService);


        [HttpGet("Groups")]
        [Authorize(Roles = "Admin, Authority, ClassPrincipal, Teacher")]
        public async Task<ActionResult> ExtractGroups() => await ExtractData<Group>(GroupsLoadExtractService);


        [HttpGet("Students")]
        [Authorize(Roles = "Admin, Authority, Teacher")]
        public async Task<ActionResult> ExtractStudents() => await ExtractData<Student>(StudentsLoadExtractService);



        [HttpPost("Users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> LoadUsers() => await LoadData<User>(UsersLoadExtractService);

        [HttpPost("Groups")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> LoadGroups() => await LoadData<Group>(GroupsLoadExtractService);


        [HttpPost("Meetings")]
        [Authorize(Roles = "Admin, Teacher, ClassPrincipal")]
        public async Task<ActionResult> LoadMeetings() => await LoadData<Meeting>(MeetingsLoadExtractService);

        [HttpPost("Students")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> LoadStudents() => await LoadData<Student>(StudentsLoadExtractService);



        protected async virtual Task<ActionResult> ExtractData<T>(ILoadExtractService<T> dataService) where T : DomainObject
        {
            object? result = await dataService.Extract();

            return result is null ? NoContent() : Json(result);
        }

        protected async virtual Task<ActionResult> LoadData<T>(ILoadExtractService<T> dataService) where T : DomainObject
        {
            IEnumerable<T>? data = await Request.ReadFromJsonAsync<IEnumerable<T>>();

            if (data is null)
                return BadRequest(ApiErrorCodes.InvalidData);

            IEnumerable<T>? skippedRows = await dataService.Load(data);

            return skippedRows is null || skippedRows.Count() == 0 ? Ok() : Json(skippedRows);
        }
    }
}
