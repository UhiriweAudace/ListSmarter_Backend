﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;
using ListSmarter.Services.Interfaces;

namespace ListSmarter.Controllers
{
    public class TaskController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public List<TaskDto> GetTasks()
        {
            return _taskService.GetTasks().ToList();
        }

        public TaskDto GetTask(string taskId)
        {
            return _taskService.GetTask(taskId);
        }

        public TaskDto CreateTask(TaskDto task)
        {
            return _taskService.CreateTask(task);
        }

        public TaskDto UpdateTask(string taskId, TaskDto task)
        {
            return _taskService.UpdateTask(taskId, task);
        }

        public TaskDto DeleteTask(string taskId)
        {
            return _taskService.DeleteTask(taskId);
        }

        
        public TaskDto AssignUserToTask(string taskId, UserDto user)
        {
            return _taskService.AssignUserToTask(taskId, user);
        }

        public TaskDto AssignBucketToTask(string taskId, BucketDto bucket)
        {
            return _taskService.AssignBucketToTask(taskId , bucket);
        }

        public TaskDto UpdateTaskStatus(string taskId, string status)
        {
            return _taskService.UpdateTaskStatus(taskId, status);
        }
    }
}
