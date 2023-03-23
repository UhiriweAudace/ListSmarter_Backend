using Moq;
using FluentAssertions;
using AutoMapper;
using ListSmarter.Models;
using ListSmarter.Models.Validators;
using System.Collections.Generic;
using ListSmarter.Repositories.Models;
using ListSmarter.Services.Interfaces;
using ListSmarter.Services;
using ListSmarter.Repositories;
using ListSmarter.Common;

namespace ListSmarter.Test;

public class UserServiceTest
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserServiceTest()
    {
        _mapper = new MapperConfiguration((cfg) =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper();
        _userService = new UserService(new UserRepository(_mapper), new UserDtoValidator() );
        foreach(User user in GetUsersDummyData().ToList())
        {
            Database.UserDbList.Add(user);
        };
    }

    [Fact]
    public void GetUsers_list()
    {

        //act
        var userResult = _mapper.Map<List<User>>(_userService.GetUsers());

        //assertions
        userResult.Should().NotBeNull();
        userResult.Count.Should().BeGreaterThan(1);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    public void GetUserById(string userId)
    {
        //act
        var userResult = _userService.GetUser(userId);

        //assertions
        userResult.Should().NotBeNull();
        userResult.Id.Should().Be(Convert.ToInt32(userId));
    }

    [Theory]
    [InlineData("320")]
    public void GetUserById_ThrowNotFoundError(string userId)
    {
        //act
        Action userResult = () =>_userService.GetUser(userId);

        //assertions
        userResult.Should().Throw<ArgumentException>().WithMessage($"User with ID 320 not found");
    }

    [Theory]
    [InlineData(null)]
    public void GetUserById_ThrowError_ForMissing_UserId(string userId)
    {
        //act
        Action userResult = () => _userService.GetUser(userId);

        //assertions
        userResult.Should().Throw<Exception>().WithMessage($"User_Error: User ID is missing");
    }

    [Theory]
    [InlineData("qerq42")]
    public void GetUserById_ThrowError_For_InvalidUserId(string userId)
    {
        //act
        var userResult = () => _userService.GetUser(userId);

        //assertions
        userResult.Should().Throw<Exception>().WithMessage($"User_Error: User ID should be a number");
    }

    [Theory]
    [InlineData("Miller", "Cobby")]
    [InlineData("Malcom", "Nicky")]
    public void CreateUser_test(string firstName, string lastName)
    {
        UserDto newUser = new UserDto() { FirstName = firstName, LastName = lastName };

        //act
        var userResult = _mapper.Map<User>(_userService.CreateUser(newUser));

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be(firstName);
        userResult.LastName.Should().Be(lastName);
    }

    [Theory]
    [InlineData("1", "Miller", "Cobby")]
    [InlineData("2", "Bradly", "Jimmy")]
    [InlineData("3", "Ronny", "Mark")]
    public void UpdateUser_test(string userId, string firstName, string lastName)
    {
        UserDto updatedUser = new UserDto() { FirstName = firstName, LastName = lastName };

        //act
        var userResult = _mapper.Map<User>(_userService.UpdateUser(userId, updatedUser));

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be(firstName);
        userResult.Id.Should().Be(Convert.ToInt32(userId));
    }

    [Theory]
    [InlineData("2")]
    public void DeleteUser_test(string userId)
    {
        //act
        var userResult = _mapper.Map<User>(_userService.DeleteUser(userId));

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be("Bradly");
        userResult.Id.Should().Be(2);
    }

    private List<User> GetUsersDummyData()
    {
        List<User> usersData = new List<User>()
        {
            new User{
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
            },
            new User{
                Id = 2,
                FirstName = "Peter",
                LastName = "Damian",
            },
            new User{
                Id = 3,
                FirstName = "Lastier",
                LastName = "Micky",
            }
        };
        return usersData;
    }

}
