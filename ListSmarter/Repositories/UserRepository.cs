using AutoMapper;
using ListSmarter.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Common;

namespace ListSmarter.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IMapper _mapper;
        private List<User> _users;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
            _users = Database.UserDbList;
        }

        public UserDto Create(UserDto user)
        {
            User newUser = _mapper.Map<User>(user);
            newUser.Id = _users.Count + 1;
            _users.Add(newUser);
            return _mapper.Map<UserDto>(newUser);
        }

        public UserDto Delete(int userId)
        {
            User userToRemove = _users.FirstOrDefault(user => user.Id == userId);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
                return _mapper.Map<UserDto>(userToRemove);
            }

            return null;
        }

        public IList<UserDto> GetAll()
        {
            return _mapper.Map<List<UserDto>>(_users);
        }

        public UserDto GetById(int userId)
        {
            User user = _users.FirstOrDefault(user => user.Id == userId);
            if (user != null)
            {
                return _mapper.Map<UserDto>(user);
            }

            return null;
        }

        public UserDto Update(int userId, UserDto userObj)
        {
            User user = _users.FirstOrDefault(user => user.Id == userId);
            if (user != null)
            {
                user.FirstName = userObj?.FirstName ?? user.FirstName;
                user.LastName = userObj?.LastName ?? user.LastName;
                return _mapper.Map<UserDto>(user);
            }

            return null;
        }
    }
}
