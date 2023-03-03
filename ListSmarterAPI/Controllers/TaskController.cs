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

        public TaskController(ITaskService taskService) => _taskService = taskService;

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

    }
}
