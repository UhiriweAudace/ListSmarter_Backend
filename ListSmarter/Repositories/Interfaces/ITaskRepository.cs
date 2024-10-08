﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;

namespace ListSmarter.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        IList<TaskDto> GetAll();
        TaskDto GetById(int taskId);
        TaskDto Create(TaskDto task);
        TaskDto Update(int taskId, TaskDto task);
        TaskDto Delete(int taskId);
    }
}
