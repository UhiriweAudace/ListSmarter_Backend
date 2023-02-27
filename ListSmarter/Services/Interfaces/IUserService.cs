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
        UserDto CreateUser(UserDto person);
        UserDto UpdateUser(string userId, UserDto person);
        UserDto DeleteUser(string userId);
    }
}
