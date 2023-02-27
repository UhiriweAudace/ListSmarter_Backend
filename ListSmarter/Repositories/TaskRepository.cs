﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;
using ListSmarter.Common;
using ListSmarter.Repositories.Interfaces;

namespace ListSmarter.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMapper _mapper;
        private List<Repositories.Models.Task> _tasks;

        public TaskRepository(IMapper mapper)
        {
            _mapper = mapper;
            _tasks = Database.TaskDbList;
        }

        public TaskDto Create(TaskDto task)
        {
            Models.Task newTask = _mapper.Map<Models.Task>(task);
            int taskLength = GetAll().ToList().Count;
            newTask.Id = taskLength + 1;
            _tasks.Add(newTask);
            return _mapper.Map<TaskDto>(newTask);
        }

        public TaskDto Delete(int taskId)
        {
            Models.Task taskToRemove = _tasks.First(task => task.Id == taskId);
            if (taskToRemove != null)
            {
                _tasks.Remove(taskToRemove);
                return _mapper.Map<TaskDto>(taskToRemove);
            }

            return null;
        }

        public IList<TaskDto> GetAll()
        {
            return _mapper.Map<List<TaskDto>>(_tasks);
        }

        public TaskDto GetById(int taskId)
        {
            Models.Task task = _tasks.First(task => task.Id == taskId);
            if (task != null)
            {
                return _mapper.Map<TaskDto>(task);
            }

            return null;
        }

        public TaskDto Update(int taskId, TaskDto taskData)
        {
            Models.Task task = _tasks.First(task => task.Id == taskId);
            if (task != null)
            {
                task.Title = taskData?.Title ?? task.Title;
                task.Description = taskData?.Description ?? task.Description;
                task.Assignee = _mapper.Map<Models.User>(taskData?.Assignee);
                task.Bucket = _mapper.Map<Models.Bucket>(taskData?.Bucket);
                return _mapper.Map<TaskDto>(task);
            }

            return null;
        }

    }
}
