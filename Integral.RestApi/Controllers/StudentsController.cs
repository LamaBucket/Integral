using Integral.Domain;
using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private IDataService<Student> _studentsDataService;

        public StudentsController(IDataService<Student> studentsDataService)
        {
            _studentsDataService = studentsDataService;
        }

        [Authorize(Roles = "Admin, Authority, Teacher")]
        [HttpGet]
        public async Task<ActionResult> GetData(int? id = null)
        {
            object? data;

            if (!id.HasValue)
                data = await _studentsDataService.GetAll();
            else
                data = await _studentsDataService.Get(id.Value);

            if (data is null)
                return NoContent();
            else
                return Json(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (!await _studentsDataService.ItemExists(id))
                return BadRequest(ApiErrorCodes.StudentNotExist);


            return Ok(await _studentsDataService.Delete(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateStudent(string firstName, string secondName, string? thirdName = null)
        {
            Student entity = new(firstName, secondName, thirdName);

            Student result = await _studentsDataService.Create(entity);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateStudent(int id, string firstName, string secondName, string? thirdName = null)
        {
            if (!await _studentsDataService.ItemExists(id))
                return BadRequest(ApiErrorCodes.StudentNotExist);

            Student entity = new(firstName, secondName, thirdName);

            Student result = await _studentsDataService.Update(id, entity);

            return Ok(result);
        }
    }
}
