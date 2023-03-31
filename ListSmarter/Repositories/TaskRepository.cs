using AutoMapper;
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
        private List<Repositories.Models.User> _users;
        private List<Repositories.Models.Bucket> _buckets;

        public TaskRepository(IMapper mapper)
        {
            _mapper = mapper;
            _tasks = Database.TaskDbList;
            _users = Database.UserDbList;
            _buckets = Database.BucketDbList;
        }

        public TaskDto Create(TaskDto task)
        {
            Models.Task newTask = _mapper.Map<Models.Task>(task);
            newTask.Id = _tasks.Count + 1;
            _tasks.Add(newTask);
            return _mapper.Map<TaskDto>(newTask);
        }

        public TaskDto Delete(int taskId)
        {
            Models.Task taskToRemove = _tasks.FirstOrDefault(task => task.Id == taskId);
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
            Models.Task task = _tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                return _mapper.Map<TaskDto>(task);
            }

            return null;
        }

        public TaskDto Update(int taskId, TaskDto taskData)
        {
            Models.Task task = _tasks.FirstOrDefault(task => task.Id == taskId);

            if (task == null)
            {
                return null;
            }

            task.Title = taskData?.Title ?? task.Title;
            task.Status = taskData?.Status ?? task.Status;
            task.Description = taskData?.Description ?? task.Description;

            var assignee = _users.FirstOrDefault(user => user.Id == taskData?.Assignee?.Id);
            if (assignee != null)
            {
                task.Assignee = assignee;
                //assignee.Tasks.Add(task);
            }

            var bucket = _buckets.FirstOrDefault(bucket => bucket.Id == taskData?.Bucket?.Id);
            if (bucket != null)
            {
                task.Bucket = bucket;
                //bucket.Tasks.Add(task);
            }

            return _mapper.Map<TaskDto>(task);
        }

    }
}
