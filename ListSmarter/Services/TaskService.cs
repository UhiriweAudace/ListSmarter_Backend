using FluentValidation;
using ListSmarter.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ListSmarter.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator<TaskDto> _taskValidator;

        public TaskService(ITaskRepository task, IValidator<TaskDto> taskValidator)
        {
            _taskRepository = task;
            _taskValidator = taskValidator ?? throw new ArgumentException();
        }

        public TaskDto CreateTask(TaskDto task)
        {
            _taskValidator.ValidateAndThrow(task);
            return _taskRepository.Create(task);
        }

        public TaskDto DeleteTask(string taskId)
        {
            ValidateTaskId(taskId);
            return _taskRepository.Delete(Convert.ToInt32(taskId));
        }

        public TaskDto GetTask(string taskId)
        {
            ValidateTaskId(taskId);
            return _taskRepository.GetById(Convert.ToInt32(taskId));
        }

        public IList<TaskDto> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        public TaskDto UpdateTask(string taskId, TaskDto task)
        {
            ValidateTaskId(taskId);
            _taskValidator.ValidateAndThrow(task);
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public TaskDto AssignTaskToUser(string taskId, UserDto user)
        {
            ValidateTaskId(taskId);
            TaskDto task = new TaskDto() { Assignee= user };
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public TaskDto AssignTaskToBucket(string taskId, BucketDto bucket)
        {
            ValidateTaskId(taskId);
            TaskDto task = new TaskDto() { Bucket = bucket };
            return _taskRepository.Update(Convert.ToInt32(taskId) , task);
        }

        public TaskDto UpdateTaskStatus(string taskId, string status)
        {
            ValidateTaskId(taskId);
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

            List<string> statusList = new List<string>() { "Open", "Closed", "InProgress"};
            if(!statusList.Any(st => st == status))
            {
                throw new Exception("Task_Error: Task Status should be either Open, Closed or InProgress");
            }

        }
    }
}
