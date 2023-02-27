using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services.Interfaces
{
    public interface IBucketService
    {
        IList<BucketDto> GetBuckets();
        BucketDto GetBucket(string bucketId);
        BucketDto UpdateBucket(string bucketId, BucketDto bucket);
        BucketDto CreateBucket(BucketDto bucket);
        BucketDto DeleteBucket(string bucketId);
    }
}
