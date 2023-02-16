using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories
{
     public interface ITask
    {
        IList<TaskDto> GetAll();
        TaskDto GetById(int id);
        void Create(TaskDto task);
        void Update(TaskDto task);
        void Delete(int id);
    }
}
