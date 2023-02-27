using System;
using ListSmarter.Repositories.Models;

namespace ListSmarter.Common
{
	public static class Database
	{
        public static List<Bucket> BucketDbList = new();
        public static List<Repositories.Models.Task> TaskDbList = new();
        public static List<User> UserDbList = new();
    }
}

