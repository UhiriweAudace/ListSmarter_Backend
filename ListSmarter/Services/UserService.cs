﻿using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Text.RegularExpressions;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;
using ListSmarter.Repositories.Models;
using FluentValidation.Results;

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
            _userValidator.ValidateAndThrow(user);
            return _userRepository.Create(user);
        }

        public UserDto DeleteUser(string userId)
        {
            GetUser(userId);
            return _userRepository.Delete(Convert.ToInt32(userId));
        }

        public UserDto GetUser(string userId)
        {

            ValidateUserId(userId);
            UserDto user = _userRepository.GetById(Convert.ToInt32(userId));
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} not found");
            }

            return user;
        }

        public IList<UserDto> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public UserDto UpdateUser(string userId, UserDto user)
        {
            GetUser(userId);
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
        }
    }
}
