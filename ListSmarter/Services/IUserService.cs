using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services
{
    public interface IUserService
    {
        IList<UserDto> GetUsers();
        UserDto GetUser(int id);
        UserDto CreateUser(UserDto person);
        void UpdateUser(int personId, UserDto person);
        UserDto DeleteUser(int personId);
    }
}
