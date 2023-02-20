using FluentValidation;
using ListSmarter.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var validatePerson = _taskValidator.Validate(task);
            if (!(validatePerson.IsValid)){
                throw new Exception("Task_Error: Task ID should be a number");
            }

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

        public TaskDto UpdateTaskStatus(string taskId, StatusEnum status)
        {
            ValidateTaskId(taskId);
            TaskDto task = new TaskDto() { Status = status };
            return _taskRepository.Update(Convert.ToInt32(taskId), task);
        }

        public void ValidateTaskId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception($"Task_Error: Task ID is missing");
            };

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception($"Task_Error: Task ID is missing");
            };

            if (!(Regex.IsMatch(id, @"^[0-9]+$", RegexOptions.Singleline)))
            {
                throw new Exception($"Task_Error: Task ID should be a number");
            }
        }
    }
}
