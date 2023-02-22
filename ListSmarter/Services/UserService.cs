using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Text.RegularExpressions;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;

namespace ListSmarter.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _userValidator;
        public UserService(IUserRepository user, IValidator<UserDto> userValidator)
        {
            _userRepository = user;
            _userValidator = userValidator ?? throw new ArgumentException();
        }
        public UserDto CreateUser(UserDto user)
        {
            return _userRepository.Create(user);
        }

        public UserDto DeleteUser(string userId)
        {
            ValidateUserId(userId);
            return _userRepository.Delete(Convert.ToInt32(userId));
        }

        public UserDto GetUser(string userId)
        {
            ValidateUserId(userId);
            return _userRepository.GetById(Convert.ToInt32(userId));
        }

        public IList<UserDto> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public UserDto UpdateUser(string userId, UserDto user)
        {

            ValidateUserId(userId);
            var validateUser = _userValidator.Validate(user);
            if (!(validateUser.IsValid))
            {
                throw new Exception("User_Error: User ID should be a number");
            }

            return _userRepository.Update(Convert.ToInt32(userId), user);
        }

        public void ValidateUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("User_Error: User ID is missing");
            };

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("User_Error: User ID is missing");
            };

            if (!(Regex.IsMatch(id, @"^[0-9]+$", RegexOptions.Singleline)))
            {
                throw new Exception("User_Error: User ID should be a number");
            }

            var validateUser = _userValidator.Validate(user);
            if (!(validateUser.IsValid))
            {
                Console.WriteLine("User_Error: User ID should be a number");
                return null;
            }

            return _userRepository.Update(Convert.ToInt32(userId), user);
        }
    }
}
