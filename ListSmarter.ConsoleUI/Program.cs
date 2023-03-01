using ListSmarter;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ListSmarter.Controllers;
using ListSmarter.Models;
using AutoMapper;
using ListSmarter.Services.Interfaces;

namespace ListSmarter.ConsoleUI
{
    class Program
    {
        static Task Main(string[] args)

        {
            using IHost host = CreateHostBuilder(args).Build();

            var bucketService = host.Services.GetService<IBucketService>() ?? throw new Exception("Bucket Service is missing!");
            var userService = host.Services.GetService<IUserService>() ?? throw new Exception("User Service is missing!");
            var taskService = host.Services.GetService<ITaskService>() ?? throw new Exception("Task Service is missing!");

            var bucketController = new BucketController(bucketService);
            var userController = new UserController(userService);
            var taskController = new TaskController(taskService);

            App(bucketController, userController, taskController);

            return host.RunAsync();

        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureServices((_, services) =>
               {
                   services.RegisterRepository();
                   services.RegisterServices();
                   services.RegisterValidators();
                   services.AddAutoMapper((config) => {}, AppDomain.CurrentDomain.GetAssemblies());
               });
        }

        public static void DisplayAppMenu(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('#', 30));
            Console.WriteLine($"# {title} {new string(' ', 10)}#");
            Console.WriteLine(new string('#', 30));
            Console.WriteLine($"# List of action supported {new string(' ', 2)}#");
            Console.WriteLine(new string('#', 30));
            Console.WriteLine("0. Menu");
            Console.WriteLine("1. List Buckets");
            Console.WriteLine("2. Get Bucket Details");
            Console.WriteLine("3. Create New Bucket");
            Console.WriteLine("4. Update a Specific Bucket");
            Console.WriteLine("5. Delete a Specific Bucket");

            Console.WriteLine("6. Get User List");
            Console.WriteLine("7. Get User Details");
            Console.WriteLine("8. Create New User");
            Console.WriteLine("9. Update a Specific User");
            Console.WriteLine("10. Delete a Specific User");
            
            Console.WriteLine("11. Get Task List");
            Console.WriteLine("12. Get Task Details");
            Console.WriteLine("13. Create New Task");
            Console.WriteLine("14. Update a Specific Task");
            Console.WriteLine("15. Delete a Specific Task");
            Console.WriteLine("16. Update Task status (e.g: Open, Closed or InProgress)");
            Console.WriteLine("17. Assign Task to a Specific User");
            Console.WriteLine("18. Assign Task to a Specific Bucket");

            Console.WriteLine("00. Exit Application");
            Console.WriteLine();
        }

        public static void App(BucketController bucketController, UserController userController, TaskController taskController)
        {
            string appTitle = "List Smarter App";
            DisplayAppMenu(appTitle);
            var bucketAction = new BucketAction(bucketController);
            var userAction = new UserAction(userController);
            var taskAction = new TaskAction(taskController);
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("What is your next action number?");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        DisplayAppMenu(appTitle); break;
                    case "1":
                        bucketAction.getAll(); break;
                    case "2":
                        bucketAction.getOne(); break;
                    case "3":
                        bucketAction.create(); break;
                    case "4":
                        bucketAction.update(); break;
                    case "5":
                        bucketAction.delete(); break;
                    case "6":
                        userAction.getAll(); break;
                    case "7":
                        userAction.getOne(); break;
                    case "8":
                        userAction.create(); break;
                    case "9":
                        userAction.update(); break;
                    case "10":
                        userAction.delete(); break;
                    case "11":
                        taskAction.getAll(); break;
                    case "12":
                        taskAction.getOne(); break;
                    case "13":
                        taskAction.create(bucketController, userController); break;
                    case "14":
                        taskAction.update(bucketController, userController); break;
                    case "15":
                        taskAction.delete(); break;
                    case "16":
                        taskAction.UpdateTaskStatus(bucketController, userController); break;
                    case "17":
                        taskAction.assignTaskToUser(bucketController, userController); break;
                    case "18":
                        taskAction.assignTaskToBucket(bucketController, userController); break;

                    case "00":
                        System.Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("No Action found for your choice! \n \n");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
