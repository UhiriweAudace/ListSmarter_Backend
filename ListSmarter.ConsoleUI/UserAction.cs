using System.Text.Json;
using System.Text.Json.Serialization;
using ListSmarter.Controllers;
using ListSmarter.Models;
using ListSmarter.Repositories.Models;

namespace ListSmarter.ConsoleUI
{
    public class UserAction
    {
        private UserController _userController;
        private JsonSerializerOptions _serializerOptions;
        public UserAction(UserController UserController)
        {
            _userController = UserController;
            _serializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
            };
        }

        public void getAll()
        {
            try
            {
                Console.WriteLine("Action -> Retrieve user list");
                _userController.GetUsers().ForEach(user =>
                {
                    Console.WriteLine(JsonSerializer.Serialize<UserDto>(user, _serializerOptions));
                });
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void getOne()
        {
            try
            {
                Console.WriteLine("Action -> Retrieve a specific user");
                Console.Write("Enter User ID: ");
                var userId = Console.ReadLine();
                var result = _userController.GetUser(userId);
                if (result != null)
                {
                    Console.WriteLine("User information");
                    Console.WriteLine(JsonSerializer.Serialize<UserDto>(result, _serializerOptions));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void create()
        {
            try
            {
                Console.WriteLine("Action -> Creating new user");
                Console.Write("Enter FirstName: ");
                string FirstName = Console.ReadLine();

                Console.Write("Enter LastName: ");
                string LastName = Console.ReadLine();

                UserDto newUser = new UserDto() { FirstName = FirstName, LastName = LastName };
                var user = _userController.CreateUser(newUser);
                if (user != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize<UserDto>(user, _serializerOptions));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void update()
        {
            try
            {
                Console.WriteLine("Action -> Updating User");
                Console.Write("Enter User ID: ");

                string userId = Console.ReadLine();
                Console.Write("Enter FirstName: ");
                string FirstName = Console.ReadLine();

                Console.Write("Enter LastName: ");
                string LastName = Console.ReadLine();

                UserDto userData = new UserDto() { FirstName = FirstName, LastName = LastName };
                var result = _userController.UpdateUser(userId, userData);
                if (result != null)
                {
                    Console.WriteLine($"User with ID {userId} was updated successfully.\n");
                }

            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void delete()
        {
            try
            {
                Console.WriteLine("Action -> Delete User");
                Console.WriteLine("Enter User ID: ");
                string userId = Console.ReadLine();

                var result = _userController.DeleteUser(userId);
                if (result != null)
                {
                    Console.WriteLine($"Bucket with ID {userId} was deleted successfully.\n");
                    Console.WriteLine(JsonSerializer.Serialize<UserDto>(result, _serializerOptions));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void LogError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void PressAnyKeyToContinue()
        {
            Console.Write("\nPress any key to continue...\n");
            Console.ReadKey();
        }
    }
}

