using System.Text.Json;
using ListSmarter.Controllers;
using ListSmarter.Models;
using ListSmarter.Repositories.Models;

namespace ListSmarter.ConsoleUI
{
    public class TaskAction
    {
        private TaskController _taskController;
        public TaskAction(TaskController taskController)
        {
            _taskController = taskController;
        }

        public void getAll()
        {
            try
            {
                Console.WriteLine("Action -> Retrieve task list");
                _taskController.GetTasks().ForEach(task =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(task));
                });
            }
            catch (System.Exception e)
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
                Console.WriteLine("Action -> Retrieve a specific task");
                Console.Write("Enter Task ID: ");
                var taskId = Console.ReadLine();

                var result = _taskController.GetTask(taskId);
                if (result != null)
                {
                    Console.WriteLine("Task information");
                    Console.WriteLine(JsonSerializer.Serialize<TaskDto>(result));
                }
            }
            catch (System.Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void create(BucketController _bucketController, UserController _userController)
        {
            try
            {
                Console.WriteLine("Action -> Creating new task");
                Console.Write("Enter Title: ");
                string Title = Console.ReadLine();

                Console.Write("Enter Description: ");
                string Description = Console.ReadLine();


                TaskDto newTask = new TaskDto() { Title = Title, Description = Description, Status = StatusEnum.None };
                var result = _taskController.CreateTask(newTask);
                if (result != null)
                {
                    Console.WriteLine($"Task with ID {result.Id} was created successfully.\n");
                }
            }
            catch (System.Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void update(BucketController _bucketController, UserController _userController)
        {
            try
            {
                Console.WriteLine("Action -> Updating User");
                Console.Write("Enter TaskId: ");
                string taskId = Console.ReadLine();

                Console.Write("Enter Title: ");
                string Title = Console.ReadLine();

                Console.Write("Enter Description: ");
                string Description = Console.ReadLine();

                TaskDto taskData = new TaskDto() { Title = Title, Description = Description };
                var result = _taskController.UpdateTask(taskId, taskData);
                if (result != null)
                {
                    Console.WriteLine($"Task with ID {taskId} was updated successfully.\n");
                }
            }
            catch (System.Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void UpdateTaskStatus(BucketController _bucketController, UserController _userController)
        {
            try
            {
                Console.WriteLine("Action -> Updating Task Status");
                Console.Write("Enter TaskId: ");
                string taskId = Console.ReadLine();

                Console.Write("Enter Status: ");
                string Status = Console.ReadLine();

                var result = _taskController.UpdateTaskStatus(taskId, Status);
                if (result != null)
                {
                    Console.WriteLine($"Task with ID {taskId} was updated successfully.\n");
                }
            }
            catch (System.Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }


        public void assignTaskToUser(BucketController _bucketController, UserController _userController)
        {
            try
            {
                Console.WriteLine("Action -> Assign A Task To A Specific User");
                Console.Write("Enter TaskId: ");
                string taskId = Console.ReadLine();

                Console.Write("Enter UserId: ");
                string userId = Console.ReadLine();

                UserDto user = _userController.GetUser(userId);
                var result = _taskController.AssignTaskToUser(taskId, user);
                if (result != null)
                {
                    user.Tasks?.Add(result);
                    _userController.UpdateUser(userId, user);
                    Console.WriteLine($"Task with ID {taskId} was updated successfully.\n");
                }
            }
            catch (System.Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }


        public void assignTaskToBucket(BucketController _bucketController, UserController _userController)
        {
            try
            {
                Console.WriteLine("Action -> Assign A Task To A Specific Bucket");
                Console.Write("Enter TaskId: ");
                string taskId = Console.ReadLine();

                Console.Write("Enter BucketId: ");
                string bucketId = Console.ReadLine();

                BucketDto bucket = _bucketController.GetBucket(bucketId);
                TaskDto result = _taskController.AssignTaskToBucket(taskId, bucket);
                if (result != null)
                {
                    if(bucket.Tasks != null && bucket.Tasks.ToList().Count >=10)
                    {
                        throw new Exception("Bucket should have 10 tasks maximum");
                    }

                    bucket.Tasks?.Add(result);
                    _bucketController.UpdateBucket(bucketId, bucket);
                    Console.WriteLine($"Task with ID {taskId} was updated successfully.\n");
                }
            }
            catch (System.Exception e)
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
                string taskId = Console.ReadLine();

                var result = _taskController.DeleteTask(taskId);
                if (result != null)
                {
                    Console.WriteLine($"Task with ID {taskId} was deleted successfully.\n");
                    Console.WriteLine(JsonSerializer.Serialize<TaskDto>(result));
                }

            }
            catch (System.Exception e)
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

