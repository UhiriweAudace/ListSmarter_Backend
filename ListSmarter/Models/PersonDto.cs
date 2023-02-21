using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Repositories;

namespace ListSmarter.Models
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<ITask>? tasks { get; set; }
    }
}
