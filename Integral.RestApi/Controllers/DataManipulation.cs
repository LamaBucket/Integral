using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Integral.RestApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DataManipulation : Controller
    {
        public DataManipulation(IDataManipulationService<User> usersDataManipulationService, IDataManipulationService<Meeting> meetingsDataManipulationService, IDataManipulationService<Group> groupsDataManipulationService, IDataManipulationService<Student> studentsDataManipulationService)
        {
            UsersDataManipulationService = usersDataManipulationService;
            MeetingsDataManipulationService = meetingsDataManipulationService;
            GroupsDataManipulationService = groupsDataManipulationService;
            StudentsDataManipulationService = studentsDataManipulationService;
        }

        public IDataManipulationService<User> UsersDataManipulationService { get; set; }

        public IDataManipulationService<Meeting> MeetingsDataManipulationService { get; set; }

        public IDataManipulationService<Group> GroupsDataManipulationService { get; set; }

        public IDataManipulationService<Student> StudentsDataManipulationService { get; set; }

        [HttpPost]
        public async Task<ActionResult> LoadData(DataManipulationTargetType targetType)
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string data = await reader.ReadToEndAsync();

                switch (targetType)
                {
                    case DataManipulationTargetType.User:
                        var users = JsonConvert.DeserializeObject<IEnumerable<User>>(data);

                        if(users is null)
                            return BadRequest(ApiErrorCodes.InvalidData);

                        DataLoadResult<User> usersResult = await UsersDataManipulationService.Load(users);

                        return Ok(usersResult);
                    case DataManipulationTargetType.Group:
                        var groups = JsonConvert.DeserializeObject<IEnumerable<Group>>(data);

                        if (groups is null)
                            return BadRequest(ApiErrorCodes.InvalidData);

                        DataLoadResult<Group> groupsResult = await GroupsDataManipulationService.Load(groups);

                        return Ok(groupsResult);
                    case DataManipulationTargetType.Student:
                        var students = JsonConvert.DeserializeObject<IEnumerable<Student>>(data);

                        if (students is null)
                            return BadRequest(ApiErrorCodes.InvalidData);

                        DataLoadResult<Student> studentsResult = await StudentsDataManipulationService.Load(students);

                        return Ok(studentsResult);
                    case DataManipulationTargetType.Meeting:
                        var meetings = JsonConvert.DeserializeObject<IEnumerable<Meeting>>(data);

                        if (meetings is null)
                            return BadRequest(ApiErrorCodes.InvalidData);

                        DataLoadResult<Meeting> meetingsResult = await MeetingsDataManipulationService.Load(meetings);

                        return Ok(meetingsResult);
                }
                
            }
             
            return BadRequest(ApiErrorCodes.InvalidTargetType);
        }

        [HttpGet]
        public async Task<ActionResult> ExtractData(DataManipulationTargetType targetType)
        {
            object? data;

            switch (targetType)
            {
                case DataManipulationTargetType.User:
                    data = await UsersDataManipulationService.Extract();
                    break;
                case DataManipulationTargetType.Group:
                    data = await GroupsDataManipulationService.Extract();
                    break;
                case DataManipulationTargetType.Student:
                    data = await StudentsDataManipulationService.Extract();
                    break;
                case DataManipulationTargetType.Meeting:
                    data = await MeetingsDataManipulationService.Extract();
                    break;
                default:
                    return BadRequest(ApiErrorCodes.InvalidTargetType);
            }

            return data is null ? NoContent() : Json(data);
        }
    }
}
