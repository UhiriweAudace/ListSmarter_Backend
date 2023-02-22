using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services
{
    public interface ITaskService
    {
        IList<TaskDto> GetTasks();
        TaskDto GetTask(string taskId);
        TaskDto CreateTask(TaskDto task);
        TaskDto UpdateTask(string taskId, TaskDto task);
        TaskDto DeleteTask(string taskId);
    }
}
