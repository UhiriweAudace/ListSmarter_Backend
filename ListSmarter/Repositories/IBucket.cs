using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories
{
    public interface IBucket
    {
        IList<BucketDto> GetAll();
        BucketDto GetById(int id);
        void Create(BucketDto bucket);
        void Update(BucketDto bucket);
        void Delete(int id);
    }
}
