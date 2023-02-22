using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Models
{
    public class TaskDto
    {
        public int taskId { get; set; }
        public BucketDto? Bucket { get; set; }
        public UserDto? Assignee { get; set; }
        public string Title { get; set; }
        public StatusEnum? Status { get; set; }
        public string Description { get; set; }
    }
}
