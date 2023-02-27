using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IList<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto Create(UserDto person);
        UserDto Update(int id, UserDto person);
        UserDto Delete(int id);
    }
}
