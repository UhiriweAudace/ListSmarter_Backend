using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListSmarter.Models;
using ListSmarter.Services;
using ListSmarter.Services.Interfaces;
using ListSmarterAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListSmarterAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> getAllUsers()
        {
            return await Task.FromResult(Ok(_userService.GetUsers().ToList()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_userService.GetUser(id)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserModel userToUpdate)
        {
            try
            {
                UserDto user = new UserDto() { FirstName = userToUpdate.FirstName, LastName = userToUpdate.LastName };
                return await Task.FromResult(Ok(_userService.CreateUser(user)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update([FromRoute] string id, [FromBody] UserModel userToUpdate)
        {
            try
            {
                UserDto user = new UserDto() { FirstName = userToUpdate.FirstName, LastName = userToUpdate.LastName };
                return await Task.FromResult(Ok(_userService.UpdateUser(id, user)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> Delete([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_userService.DeleteUser(id)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
