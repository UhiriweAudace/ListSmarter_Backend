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
        TaskDto GetTask(int taskId);
        TaskDto CreateTask(TaskDto task);
        void UpdateTask(int taskId, TaskDto task);
        TaskDto DeleteTask(int taskId);
    }
}
