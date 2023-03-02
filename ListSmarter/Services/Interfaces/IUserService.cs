using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services.Interfaces
{
    public interface IUserService
    {
        IList<UserDto> GetUsers();
        UserDto GetUser(string userId);
        UserDto CreateUser(UserDto user);
        UserDto UpdateUser(string userId, UserDto user);
        UserDto DeleteUser(string userId);
    }
}
