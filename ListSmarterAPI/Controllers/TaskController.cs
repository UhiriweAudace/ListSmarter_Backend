using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListSmarter.Models;
using ListSmarter.Services.Interfaces;
using ListSmarterAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListSmarterAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;

        private IBucketService _bucketService;

        private IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService, IBucketService bucketService)
        {
            _userService = userService;
            _taskService = taskService;
            _bucketService = bucketService;
        }

        [HttpGet(Name = "GetTasks")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> getAllTasks()
        {
            return await Task.FromResult(Ok(_taskService.GetTasks().ToList()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_taskService.GetTask(id)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPost(Name = "CreateTask")]
        public async Task<ActionResult<TaskDto>> Create([FromBody] TaskModel taskData)
        {
            try
            {
                TaskDto task = new TaskDto() { Title = taskData.Title, Description = taskData.Description };
                return await Task.FromResult(Ok(_taskService.CreateTask(task)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> Update([FromRoute] string id, [FromBody] TaskModel taskData)
        {
            try
            {
                TaskDto task = new TaskDto() { Title = taskData.Title, Description = taskData.Description };
                return await Task.FromResult(Ok(_taskService.UpdateTask(id, task)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskDto>> Delete([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_taskService.DeleteTask(id)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<TaskDto>> UpdateTaskStatus([FromRoute] string id, StatusEnum status)
        {
            try
            {
                return await Task.FromResult(Ok(_taskService.UpdateTaskStatus(id, status.ToString())));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("assignBucketToTask/{taskId}/bucket/{bucketId}")]
        public async Task<ActionResult<TaskDto>> AssignBucketToTask([FromRoute] string taskId, [FromRoute] string bucketId)
        {
            try
            {
                BucketDto bucket = _bucketService.GetBucket(bucketId);
                return await Task.FromResult(Ok(_taskService.AssignBucketToTask(taskId, bucket)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("assignUserToTask/{taskId}/user/{userId}")]
        public async Task<ActionResult<TaskDto>> AssiggUserToTask([FromRoute] string taskId, [FromRoute] string userId)
        {
            try
            {
                UserDto user = _userService.GetUser(userId);
                return await Task.FromResult(Ok(_taskService.AssignUserToTask(taskId, user)));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
