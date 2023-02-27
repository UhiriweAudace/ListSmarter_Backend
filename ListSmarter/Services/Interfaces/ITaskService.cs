using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services.Interfaces
{
    public interface ITaskService
    {
        IList<TaskDto> GetTasks();
        TaskDto GetTask(string taskId);
        TaskDto CreateTask(TaskDto task);
        TaskDto UpdateTask(string taskId, TaskDto task);
        TaskDto DeleteTask(string taskId);
        TaskDto AssignTaskToUser(string taskId, UserDto user);
        TaskDto AssignTaskToBucket(string taskId, BucketDto bucket);
        TaskDto UpdateTaskStatus(string taskId, StatusEnum status);
    }
}
