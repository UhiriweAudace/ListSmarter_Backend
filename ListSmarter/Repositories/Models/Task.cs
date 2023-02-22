using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ListSmarter.Repositories.Models
{
    public class Task
    {
        public int taskId { get; set; }
        public Bucket? Bucket { get; set; }
        public User? Assignee { get; set; }
        public string Title { get; set; }
        public StatusEnum? Status { get; set; }
        public string Description { get; set; }
    }
}
