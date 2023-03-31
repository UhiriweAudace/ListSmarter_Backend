using System;
using Moq;
using FluentAssertions;
using AutoMapper;
using ListSmarter.Models;
using ListSmarter.Models.Validators;
using System.Collections.Generic;
using ListSmarter.Services.Interfaces;
using ListSmarter.Services;
using ListSmarter.Repositories;
using ListSmarter.Common;

using Task = ListSmarter.Repositories.Models.Task;
using ListSmarter.Repositories.Models;

namespace ListSmarter.Test
{
    public class TaskServiceTest
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskServiceTest()
        {
            // Initialize AutoMapper
            _mapper = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            // Initialize TaskService
            _taskService = new TaskService(new TaskRepository(_mapper), new TaskDtoValidator());

            // Seed Task DB
            User user = new User
            {
                Id = 10,
                FirstName = "Peter",
                LastName = "Damian",
            };
            Database.UserDbList.Add(user);

            // Seed Bucket DB
            Bucket bucket = new Bucket { Id = 4, Title = "Burger Builder Application" };
            Database.BucketDbList.Add(bucket);

            // Seed Task DB
            foreach (Task task in GetTasksDummyData().ToList())
            {
                Database.TaskDbList.Add(task);
            }
        }

        [Fact]
        public void GetTasks_list()
        {

            //act
            var TaskResult = _mapper.Map<List<Task>>(_taskService.GetTasks());

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Count.Should().BeGreaterThan(1);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void GetTaskById(string TaskId)
        {
            //act
            var TaskResult = _taskService.GetTask(TaskId);

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Id.Should().Be(Convert.ToInt32(TaskId));
        }

        [Theory]
        [InlineData("7890")]
        public void GetTaskById_ThrowNotFoundError(string TaskId)
        {
            //act
            Action TaskResult = () => _taskService.GetTask(TaskId);

            //assertions
            TaskResult.Should().Throw<ArgumentException>().WithMessage($"Task with ID 7890 not found");
        }

        [Theory]
        [InlineData(null)]
        public void GetTaskById_ThrowError_ForMissing_TaskId(string TaskId)
        {
            //act
            Action TaskResult = () => _taskService.GetTask(TaskId);

            //assertions
            TaskResult.Should().Throw<Exception>().WithMessage($"Task_Error: Task ID is missing");
        }

        [Theory]
        [InlineData("wq34_rtUhZ")]
        public void GetTaskById_ThrowError_For_InvalidTaskId(string TaskId)
        {
            //act
            var TaskResult = () => _taskService.GetTask(TaskId);

            //assertions
            TaskResult.Should().Throw<Exception>().WithMessage($"Task_Error: Task ID should be a number");
        }

        [Theory]
        [InlineData("Create navigation menu", "dummy description", StatusEnum.Open)]
        [InlineData("Title 2", "description 2", StatusEnum.Closed)]
        [InlineData("Title 3", "description 3", StatusEnum.None)]
        public void CreateTask_test(string title, string description, StatusEnum status)
        {
            TaskDto newTask = new TaskDto() { Title = title, Description = description, Status = status };

            //act
            var TaskResult = _mapper.Map<Task>(_taskService.CreateTask(newTask));

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Title.Should().Be(title);
            TaskResult.Status.Should().Be(status);
            TaskResult.Description.Should().Be(description);
        }

        [Theory]
        [InlineData("1", "Create navigation menu", "dummy description")]
        public void UpdateTask_test(string TaskId, string title, string description)
        {
            TaskDto updatedTask = new TaskDto() { Title = title, Description = description };

            //act
            var TaskResult = _mapper.Map<Task>(_taskService.UpdateTask(TaskId, updatedTask));

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Id.Should().Be(Convert.ToInt32(TaskId));
            TaskResult.Title.Should().Be(title);
            TaskResult.Status.Should().Be(StatusEnum.None);
            TaskResult.Description.Should().Be(description);
        }

        [Theory]
        [InlineData("2")]
        [InlineData("3")]
        public void DeleteTask_test(string TaskId)
        {
            //act
            var TaskResult = _mapper.Map<Task>(_taskService.DeleteTask(TaskId));

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Title.Should().Be($"Create landing page {TaskId}");
            TaskResult.Id.Should().Be(Convert.ToInt32(TaskId));
        }

        [Theory]
        [InlineData("1034", "1")]
        public void AssignBucketToTask_ThrowError_If_Task_Not_Found(string taskId, string bucketId)
        {
            // act
            BucketDto bucketDto = new BucketDto { Id = Convert.ToInt32(bucketId), Title = "Bucket Title 1" };
            Action TaskResult = () => _taskService.AssignBucketToTask(taskId, bucketDto);

            TaskResult.Should().Throw<Exception>().WithMessage("Task with ID 1034 not found");
        }

        [Theory]
        [InlineData("1", "1")]
        public void AssignBucketToTask_ThrowError_If_Bucket_Limit_Exceeded(string taskId, string bucketId)
        {
            // act
            BucketDto bucketDto = new BucketDto { Id = Convert.ToInt32(bucketId), Title = "Bucket Title 1" };
            Action TaskResult = () => _taskService.AssignBucketToTask(taskId, bucketDto);

            TaskResult.Should().Throw<Exception>().WithMessage("Task_Error: 10 Tasks per Bucket exceeded");
        }


        private List<Task> GetTasksDummyData()
        {
            var taskIds = Enumerable.Range(1, 10).ToList();
            List<Task> tasksData = taskIds.Select((idx) =>
            {
                return new Task
                {
                    Id = idx,
                    Status = StatusEnum.None,
                    Title = $"Create landing page {idx}",
                    Description = $"Description {idx}",
                    Bucket = new Bucket { Id = 1, Title = "Bucket Title 1" },
                    Assignee = new User { Id = idx, FirstName = $"firstname {idx}", LastName = $"lastname {idx}" }
                };
            }).ToList();
            return tasksData;
        }
    }
}

