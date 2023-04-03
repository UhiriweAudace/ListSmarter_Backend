using Moq;
using FluentAssertions;
using AutoMapper;
using ListSmarter.Models;
using ListSmarter.Models.Validators;
using ListSmarter.Repositories.Models;
using ListSmarter.Services.Interfaces;
using ListSmarter.Services;
using ListSmarter.Repositories.Interfaces;

namespace ListSmarter.Test;

public class UserServiceTest
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _mockUserRepository;

    public UserServiceTest()
    {
        _mapper = new MapperConfiguration((cfg) =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper();

        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object, new UserDtoValidator() );
    }

    [Fact]
    public void GetUsers_ShouldReturnUsersList()
    {
        var users = _mapper.Map<List<UserDto>>(GetUsersDummyData());
        _mockUserRepository.Setup((x) => x.GetAll()).Returns(users);

        //act
        var userResult = _userService.GetUsers();

        //assertions
        userResult.Should().NotBeNull();
        userResult.Count.Should().BeGreaterThan(1);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetUser_ShouldReturnUserDetails(int userId)
    {
        // Arrange
        var expectedResponse = _mapper.Map<UserDto>(GetUsersDummyData().FirstOrDefault((x) => x.Id == userId));
        _mockUserRepository.Setup(x => x.GetById(userId)).Returns(expectedResponse);
        //act
        var userResult = _userService.GetUser(userId.ToString());

        //assertions
        userResult.Should().NotBeNull();
        userResult.Id.Should().Be(Convert.ToInt32(userId));
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    public void GetUser_ShouldThrowNotFoundError(int userId)
    {
        //act
        Action userResult = () =>_userService.GetUser(userId.ToString());

        //assertions
        userResult.Should().Throw<ArgumentException>().WithMessage($"User with ID {userId} not found");
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    public void GetUser_ShouldThrowError_WhenUserIdIsMissing(string userId)
    {
        //act
        Action userResult = () => _userService.GetUser(userId);

        //assertions
        userResult.Should().Throw<Exception>().WithMessage($"User_Error: User ID is missing");
    }

    [Theory]
    [InlineData("qerq42")]
    [InlineData("xd_fr567")]
    public void GetUser_ShouldThrowError_ForInvalidUserId(string userId)
    {
        //act
        var userResult = () => _userService.GetUser(userId);

        //assertions
        userResult.Should().Throw<Exception>().WithMessage($"User_Error: User ID should be a number");
    }

    [Theory]
    [InlineData("Miller", "Cobby")]
    [InlineData("Malcom", "Nicky")]
    public void CreateUser_ShouldReturnCreatedUser(string firstName, string lastName)
    {
        UserDto newUser = new UserDto() { FirstName = firstName, LastName = lastName };
        _mockUserRepository.Setup(x => x.Create(newUser)).Returns(newUser);

        //act
        var userResult = _userService.CreateUser(newUser);

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be(firstName);
        userResult.LastName.Should().Be(lastName);
    }

    [Theory]
    [InlineData(1, "Miller", "Cobby")]
    [InlineData(2, "Bradly", "Jimmy")]
    [InlineData(3, "Ronny", "Mark")]
    public void UpdateUser_ShouldReturnUpdatedUser(int userId, string firstName, string lastName)
    {
        // Arrange
        UserDto existingUserDto = _mapper.Map<UserDto>(GetUsersDummyData().FirstOrDefault(user => user.Id == userId));
        UserDto updatedUserDto = new UserDto() { FirstName = firstName, LastName = lastName };
        var expectedResponseDto = new UserDto { Id = existingUserDto.Id, FirstName = firstName, LastName = lastName};

        _mockUserRepository.Setup(x => x.GetById(userId)).Returns(updatedUserDto);
        _mockUserRepository.Setup(x => x.Update(userId, updatedUserDto)).Returns(expectedResponseDto);

        //act
        var userResult = _userService.UpdateUser(userId.ToString(), updatedUserDto);

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be(firstName);
        userResult.Id.Should().Be(Convert.ToInt32(userId));
    }

    [Theory]
    [InlineData(2, "Peter")]
    [InlineData(3, "Lastier")]
    public void DeleteUser_ShouldReturnDeletedUser(int userId, string deletedFirstName)
    {
        // Arrange
        UserDto existingUserDto = _mapper.Map<UserDto>(GetUsersDummyData().FirstOrDefault(user => user.Id==userId));

        _mockUserRepository.Setup(x => x.GetById(userId)).Returns(existingUserDto);
        _mockUserRepository.Setup(x => x.Delete(userId)).Returns(existingUserDto);

        //act
        var userResult = _userService.DeleteUser(userId.ToString());

        //assertions
        userResult.Should().NotBeNull();
        userResult.FirstName.Should().Be(deletedFirstName);
        userResult.Id.Should().Be(userId);
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
