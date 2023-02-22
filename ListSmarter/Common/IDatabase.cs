using ListSmarter.Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Common
{
    public interface IDatabase
    {
        public static List<Bucket> bucketDbList;
        public static List<Repositories.Models.Task> taskDbList;
        public static List<User> userDbList;
    }
}
