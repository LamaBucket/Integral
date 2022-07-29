using Integral.Domain;
using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IUserDataService _usersDataService;
        private IGroupDataService _groupsDataService;

        private IPasswordHashService _passwordHasher;

        public UsersController(IUserDataService usersDataService, IPasswordHashService passwordHasher, IGroupDataService groupsDataService)
        {
            _usersDataService = usersDataService;
            _passwordHasher = passwordHasher;
            _groupsDataService = groupsDataService;
        }


        [Authorize(Roles = "Admin, Authority")]
        [HttpGet]
        public async Task<ActionResult> GetData(int? id = null)
        {
            int _id = id ?? default;

            object? data;

            if (id is not null)
            {
                data = await _usersDataService.Get(_id);

            }
            else
            {
                data = await _usersDataService.GetAll();
            }

            if (data is null)
                return NotFound();
            else
                return Json(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (!await _usersDataService.ItemExists(id))
                return BadRequest(ApiErrorCodes.UserNotExist);

            bool ok = await _usersDataService.Delete(id);

            if (ok)
                return Ok();

            return StatusCode(500);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateUser(string username, [MinLength(9)] string password)
        {
            if (await _usersDataService.ItemExists(await _usersDataService.GetId(username)))
                return BadRequest(ApiErrorCodes.UserAlreadyExists);

            User entity = new(username, _passwordHasher.HashPassword(password));
            User result = await _usersDataService.Create(entity);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateUserPassword(int id, [MinLength(9)] string password)
        {
            User? user = await _usersDataService.Get(id);

            if (user is null)
                return BadRequest(ApiErrorCodes.UserNotExist);

            user.PasswordHash = _passwordHasher.HashPassword(password);

            User result = await _usersDataService.Update(id, user);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Role")]
        public async Task<ActionResult> AddUserRole(int id, Role userRole)
        {
            User? user = await _usersDataService.Get(id);

            if (user is null)
                return BadRequest(ApiErrorCodes.UserNotExist);

            if (await _usersDataService.HasRole(id, userRole))
                return BadRequest(ApiErrorCodes.UserAlreadyHasRole);

            UserRole role = new()
            {
                UserId = id,
                Role = userRole
            };

            if (user.UserRoles is null)
                user.UserRoles = new List<UserRole>() { role };
            else
                user.UserRoles.Add(role);

            User result = await _usersDataService.Update(id, user);

            return Ok(result);

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Role")]
        public async Task<ActionResult> RemoveUserRole(int id, Role userRole)
        {
            User? user = await _usersDataService.Get(id);

            if (user is null)
                return BadRequest(ApiErrorCodes.UserNotExist);

            if (user.UserRoles is null)
                return BadRequest(ApiErrorCodes.UserNotHaveRoles);

            UserRole? role = user.UserRoles.FirstOrDefault(x => x.Role == userRole);

            if (role is null)
                return BadRequest(ApiErrorCodes.UserNotHaveRole);

            if (await _groupsDataService.GetOwnedGroups(id, role.Role) is not null)
                return BadRequest(ApiErrorCodes.UserHasGroups);

            user.UserRoles.Remove(role);

            User result = await _usersDataService.Update(id, user);

            return Ok(result);
        }
    }
}
