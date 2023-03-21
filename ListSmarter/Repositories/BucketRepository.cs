using AutoMapper;
using ListSmarter.Common;
using ListSmarter.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories
{
    public class BucketRepository : IBucketRepository
    {
        private readonly IMapper _mapper;
        private List<Bucket> _buckets;

        public BucketRepository(IMapper mapper)
        {
            _mapper = mapper;
            _buckets = Database.BucketDbList;
        }

        public BucketDto Create(BucketDto bucket)
        {
            Bucket newBucket = _mapper.Map<Bucket>(bucket);
            newBucket.Id = _buckets.Count + 1;
            _buckets.Add(newBucket);
            return _mapper.Map<BucketDto>(newBucket);
        }

        public BucketDto Delete(int bucketId)
        {
            Bucket bucketToRemove = _buckets.FirstOrDefault(bucket => bucket.Id == bucketId);
            if(bucketToRemove!=null)
            {
                _buckets.Remove(bucketToRemove);
                return _mapper.Map<BucketDto>(bucketToRemove);
            }

            return null;
        }

        public IList<BucketDto> GetAll()
        {
            return _mapper.Map<List<BucketDto>>( _buckets );
        }

        public BucketDto GetById(int bucketId)
        {
            Bucket bucket = _buckets.FirstOrDefault(bucket => bucket.Id == bucketId);
            if(bucket != null ){
                return _mapper.Map<BucketDto>(bucket);
            }

            return null;
        }

        public BucketDto Update(int bucketId, BucketDto bucketObj)
        {
            Bucket bucket = _buckets.FirstOrDefault(bucket => bucket.Id == bucketId);
            if(bucket != null)
            {
                bucket.Title = bucketObj.Title ?? bucket.Title;
                return _mapper.Map<BucketDto>(bucket);
            }

            return null;
        }
    }
}
