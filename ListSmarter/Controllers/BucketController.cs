using ListSmarter.Models;
using ListSmarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Controllers
{
    public class BucketController
    {
        private readonly IBucketService _bucketService;
        public BucketController(IBucketService bucketService)
        {
            _bucketService = bucketService;
        }

        public List<BucketDto> GetBuckets()
        {
            return _bucketService.GetBuckets().ToList();
        }

        public BucketDto GetBucket(string bucketId)
        {
            return _bucketService.GetBucket(bucketId);
        }

        public BucketDto CreateBucket(BucketDto bucket)
        {
            return _bucketService.CreateBucket(bucket);
        }

        public BucketDto UpdateBucket(string bucketId, BucketDto bucket)
        {
            return _bucketService.UpdateBucket(bucketId, bucket);
        }

        public BucketDto DeleteBucket(string bucketId)
        {
            return _bucketService.DeleteBucket(bucketId);
        }
    }
}
