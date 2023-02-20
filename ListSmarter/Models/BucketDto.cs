using ListSmarter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Models
{
    public class BucketDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public List<TaskDto>? Tasks { get; set; }
    }
}
