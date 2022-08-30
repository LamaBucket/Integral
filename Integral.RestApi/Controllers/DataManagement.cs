﻿using Integral.Domain.Models;
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
    public class DataManagement : Controller
    {
        public DataManagement(ILoadExtractService<User> usersLoadExtractService,
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
        public async Task<ActionResult> ExtractUsers() => await ExtractData<User>(UsersLoadExtractService);

        [HttpGet("Meetings")]
        public async Task<ActionResult> ExtractMeetings() => await ExtractData<Meeting>(MeetingsLoadExtractService);

        [HttpGet("Groups")]
        public async Task<ActionResult> ExtractGroups() => await ExtractData<Group>(GroupsLoadExtractService);

        [HttpGet("Students")]
        public async Task<ActionResult> ExtractStudents() => await ExtractData<Student>(StudentsLoadExtractService);



        [HttpPost("Users")]
        public async Task<ActionResult> LoadUsers([FromBody] string json) => await LoadData<User>(UsersLoadExtractService, json);

        [HttpPost("Groups")]
        public async Task<ActionResult> LoadGroups([FromBody] string json) => await LoadData<Group>(GroupsLoadExtractService, json);

        [HttpPost("Meetings")]
        public async Task<ActionResult> LoadMeetings([FromBody] string json) => await LoadData<Meeting>(MeetingsLoadExtractService, json);

        [HttpPost("Students")]
        public async Task<ActionResult> LoadStudents([FromBody] string json) => await LoadData<Student>(StudentsLoadExtractService, json);



        protected async virtual Task<ActionResult> ExtractData<T>(ILoadExtractService<T> dataService) where T : DomainObject
        {
            object? result = await dataService.Extract();

            return result is null ? NoContent() : Json(result);
        }

        protected async virtual Task<ActionResult> LoadData<T>(ILoadExtractService<T> dataService, string json) where T : DomainObject
        {
            IEnumerable<T>? data = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

            if (data is null)
                return BadRequest(ApiErrorCodes.InvalidData);

            IEnumerable<T>? skippedRows = await dataService.Load(data);

            return skippedRows is null || skippedRows.Count() == 0 ? Ok() : Json(skippedRows);
        }
    }
}
