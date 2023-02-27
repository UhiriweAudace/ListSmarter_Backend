using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;
using ListSmarter.Services.Interfaces;

namespace ListSmarter.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService personService)
        {
            _userService = personService;
        }

        public List<UserDto> GetUsers()
        {
            return _userService.GetUsers().ToList();
        }

        public UserDto GetUser(string userId)
        {
            return _userService.GetUser(userId);
        }

        public UserDto CreateUser(UserDto user)
        {
            return _userService.CreateUser(user);
        }

        public UserDto UpdateUser(string userId, UserDto user)
        {
            return _userService.UpdateUser(userId, user);
        }

        public UserDto DeleteUser(string userId)
        {
            return _userService.DeleteUser(userId);
        }
    }
}
