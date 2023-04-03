using Moq;
using AutoMapper;
using FluentAssertions;
using ListSmarter.Common;
using ListSmarter.Models;
using ListSmarter.Services;
using ListSmarter.Models.Validators;
using ListSmarter.Services.Interfaces;
using ListSmarter.Repositories.Interfaces;

using Task = ListSmarter.Repositories.Models.Task;
using User = ListSmarter.Repositories.Models.User;
using Bucket = ListSmarter.Repositories.Models.Bucket;
using AutoMapper.Configuration.Annotations;

namespace ListSmarter.Test
{
    public class TaskServiceTest
    {
        private readonly User _user;
        private readonly Bucket _bucket;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public TaskServiceTest()
        {
            // Initialize AutoMapper
            _mapper = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();


            // Initialize TaskService
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object, new TaskDtoValidator());

            _user = new User { Id = 10, FirstName = "Peter", LastName = "Damian" };
            _bucket = new Bucket { Id = 4, Title = "Burger Builder Application" };
        }

        [Fact]
        public void GetTasks_ShouldReturnTasksList()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<TaskDto>>(GetTasksDummyData());
            _mockTaskRepository.Setup(x => x.GetAll()).Returns(expectedResponse);
            //act
            var TaskResult = _taskService.GetTasks();

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Count.Should().BeGreaterThan(1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetTask_ShouldReturnTaskDetails(int taskId)
        {
            // Arrange
            var expectedResponse = _mapper.Map<TaskDto>(GetTasksDummyData().FirstOrDefault(task => task.Id == taskId));
            _mockTaskRepository.Setup(x => x.GetById(taskId)).Returns(expectedResponse);

            //act
            var TaskResult = _taskService.GetTask(taskId.ToString());

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Id.Should().Be(taskId);
            TaskResult.Title.Should().Be($"Create landing page {taskId}");
        }

        [Theory]
        [InlineData(7890)]
        [InlineData(532)]
        public void GetTask_ShouldThrowNotFoundError(int taskId)
        {
            // Arrange
            _mockTaskRepository.Setup(x => x.GetById(taskId)).Throws(new ArgumentException($"Task with ID {taskId} not found"));

            //act
            Action TaskResult = () => _taskService.GetTask(taskId.ToString());

            //assertions
            TaskResult.Should().Throw<ArgumentException>().WithMessage($"Task with ID {taskId} not found");
        }

        [Theory]
        [InlineData(null)]
        public void GetTask_ShouldThrowError_WhenTaskIdIsMissing(int taskId)
        {
            _mockTaskRepository.Setup(x => x.GetById(taskId)).Throws(new ArgumentException($"Task_Error: Task ID is missing"));

            //act
            Action TaskResult = () => _taskService.GetTask(taskId.ToString());

            //assertions
            TaskResult.Should().Throw<Exception>().WithMessage("Task_Error: Task ID is missing");
        }

        [Theory]
        [InlineData("wq34_rtUhZ")]
        public void GetTask_ShouldThrowError_WhenTaskIdIsInvalid(string taskIdString)
        {
            //act
            var TaskResult = () => _taskService.GetTask(taskIdString);

            //assertions
            TaskResult.Should().Throw<Exception>().WithMessage("Task_Error: Task ID should be a number");
        }

        [Theory]
        [InlineData(1, "Create navigation menu", "dummy description", StatusEnum.Open)]
        [InlineData(2, "Title 2", "description 2", StatusEnum.Closed)]
        [InlineData(3, "Title 3", "description 3", StatusEnum.None)]
        public void CreateTask_test(int taskId, string title, string description, StatusEnum status)
        {
            TaskDto newTaskDto = new TaskDto() { Title = title, Description = description, Status = status };
            TaskDto expectedResponseDto = new TaskDto() { Id = taskId, Title = title, Description = description, Status = status };
            _mockTaskRepository.Setup(x => x.Create(newTaskDto)).Returns(expectedResponseDto);

            //act
            var TaskResult = _mapper.Map<Task>(_taskService.CreateTask(newTaskDto));

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Id.Should().Be(taskId);
            TaskResult.Title.Should().Be(title);
            TaskResult.Status.Should().Be(status);
            TaskResult.Description.Should().Be(description);
        }

        [Theory]
        [InlineData(1, "Create navigation menu", "dummy description")]
        [InlineData(2, "Create aboutus menu", "dummy description")]
        public void UpdateTask_ShouldReturnUpdatedTask(int taskId, string title, string description)
        {
            TaskDto updatedTaskDto = new TaskDto() { Title = title, Description = description };
            TaskDto existingTaskDto = _mapper.Map<TaskDto>(GetTasksDummyData().FirstOrDefault(task => task.Id == taskId));
            TaskDto expectedResponseDto = new TaskDto() { Id = taskId, Title = title, Description = description, Status = existingTaskDto.Status };

            _mockTaskRepository.Setup(x => x.GetById(taskId)).Returns(existingTaskDto);
            _mockTaskRepository.Setup(x => x.Update(taskId, updatedTaskDto)).Returns(expectedResponseDto);

            //act
            var TaskResult = _taskService.UpdateTask(taskId.ToString(), updatedTaskDto);

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Id.Should().Be(taskId);
            TaskResult.Title.Should().Be(title);
            TaskResult.Status.Should().Be(StatusEnum.None);
            TaskResult.Description.Should().Be(description);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteTask_ShouldReturnDeletedTask(int taskId)
        {
            TaskDto expectedResponseDto = _mapper.Map<TaskDto>(GetTasksDummyData().FirstOrDefault(task => task.Id == taskId));
            _mockTaskRepository.Setup(x => x.GetById(taskId)).Returns(expectedResponseDto);
            _mockTaskRepository.Setup(x => x.Delete(taskId)).Returns(expectedResponseDto);

            //act
            var TaskResult = _taskService.DeleteTask(taskId.ToString());

            //assertions
            TaskResult.Should().NotBeNull();
            TaskResult.Title.Should().Be($"Create landing page {taskId}");
            TaskResult.Id.Should().Be(taskId);
        }

        [Theory]
        [InlineData(1034, 1)]
        [InlineData(104, 2)]
        public void AssignBucketToTask_ShouldThrowError_WhenTaskNotFound(int taskId, int bucketId)
        {
            // act
            BucketDto bucketDto = new BucketDto { Id = bucketId, Title = $"Bucket Title {bucketId}" };
            Action TaskResult = () => _taskService.AssignBucketToTask(taskId.ToString(), bucketDto);

            TaskResult.Should().Throw<Exception>().WithMessage($"Task with ID {taskId} not found");
        }

        [Theory(Skip = "skip it")]
        [InlineData(1, 1)]
        public void AssignBucketToTask_ThrowError_WhenBucketLimitReached(int taskId, int bucketId)
        {
            // Arrange
            TaskDto existingTaskDto = _mapper.Map<TaskDto>(GetTasksDummyData().FirstOrDefault(task => task.Id == taskId));
            _mockTaskRepository.Setup(x => x.GetById(taskId)).Returns(existingTaskDto);

            // act
            BucketDto bucketDto = new BucketDto { Id = bucketId, Title = "Bucket Title 1" };
            Action TaskResult = () => _taskService.AssignBucketToTask(taskId.ToString(), bucketDto);

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

