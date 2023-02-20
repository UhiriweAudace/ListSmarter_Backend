using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services
{
    public interface IBucketService
    {
        IList<BucketDto> GetBuckets();
        BucketDto GetBucket(int bucketId);
        void UpdateBucket(int bucketId, BucketDto bucket);
        BucketDto CreateBucket(BucketDto bucket);
        BucketDto DeleteBucket(int bucketId);
    }
}
