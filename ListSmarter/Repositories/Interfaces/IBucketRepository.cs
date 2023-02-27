using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSmarter.Models;

namespace ListSmarter.Repositories.Interfaces
{
    public interface IBucketRepository
    {
        IList<BucketDto> GetAll();
        BucketDto GetById(int id);
        BucketDto Create(BucketDto bucket);
        BucketDto Update(int id, BucketDto bucket);
        BucketDto Delete(int id);
    }
}
