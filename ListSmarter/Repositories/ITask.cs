using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;

namespace ListSmarter.Repositories
{
     public interface ITask
    {
        IList<TaskDto> GetAll();
        TaskDto GetById(int id);
        TaskDto Create(TaskDto task);
        void Update(int id, TaskDto task);
        TaskDto Delete(int id);
    }
}
