using System.Text.RegularExpressions;
using FluentValidation;
using ListSmarter.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;
using ListSmarter.Common;
using Task = ListSmarter.Repositories.Models.Task;

namespace ListSmarter.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator<TaskDto> _taskValidator;
        private List<Task> _tasks;

        public TaskService(ITaskRepository task, IValidator<TaskDto> taskValidator)
        {
            _taskRepository = task;
            _taskValidator = taskValidator ?? throw new ArgumentException();
            _tasks = Database.TaskDbList;
        }

        public TaskDto CreateTask(TaskDto task)
        {
            _taskValidator.ValidateAndThrow(task);
            return _taskRepository.Create(task);
        }

        public TaskDto DeleteTask(string taskId)
        {
            TaskDto task = GetTask(taskId);
            return _taskRepository.Delete(task.Id);
        }

        public TaskDto GetTask(string taskId)
        {
            ValidateTaskId(taskId);
            TaskDto task = _taskRepository.GetById(Convert.ToInt32(taskId));
            if (task == null)
            {
                throw new ArgumentException($"Task with ID {taskId} not found");
            }
            return task;
        }

        public IList<TaskDto> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        public TaskDto UpdateTask(string taskId, TaskDto task)
        {
            GetTask(taskId);
            _taskValidator.ValidateAndThrow(task);
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public TaskDto AssignUserToTask(string taskId, UserDto user)
        {
            GetTask(taskId);
            TaskDto task = new TaskDto() { Assignee = user };
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public TaskDto AssignBucketToTask(string taskId, BucketDto bucket)
        {
            GetTask(taskId);
            ValidateBucketFullness(bucket.Id);
            TaskDto task = new TaskDto() { Bucket = bucket };
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public TaskDto UpdateTaskStatus(string taskId, string status)
        {
            GetTask(taskId);
            ValidateTaskStatus(status);

            StatusEnum Status = Enum.Parse<StatusEnum>(status);
            TaskDto task = new TaskDto() { Status = Status };
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public void ValidateTaskId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Task_Error: Task ID is missing");
            };

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Task_Error: Task ID is missing");
            };

            bool isValid = Regex.IsMatch(id, @"^[0-9]+$", RegexOptions.None);
            if (!isValid)
            {
                throw new Exception("Task_Error: Task ID should be a number");
            }
        }

        public void ValidateTaskStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new Exception("Task_Error: Task Status should not be empty");
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                throw new Exception("Task_Error: Task Status should not be empty");
            }

            List<string> statusList = new List<string>() { "Open", "Closed", "InProgress" };
            if (!statusList.Any(st => st == status))
            {
                throw new Exception("Task_Error: Task Status should be either Open, Closed or InProgress");
            }
        }

        public void ValidateBucketFullness(int bucketId)
        {
            int taskCount = _tasks.Aggregate(0, (prev, cur) =>
            {
                if (cur?.Bucket?.Id == bucketId)
                {
                    int count = prev++;
                }

                Console.WriteLine("=> TaskCount <==", prev);
   
                return prev;
            });

            if (taskCount >= 10)
            {
                throw new Exception("Task_Error: 10 Tasks per Bucket exceeded");
            }
        }
    }
}
