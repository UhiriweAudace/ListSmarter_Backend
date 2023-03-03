using FluentValidation;
using FluentValidation.Results;
using ListSmarter.Common;
using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services.Interfaces;
using ListSmarter.Repositories.Models;

namespace ListSmarter.Services
{
    public class BucketService : IBucketService
    {
        private readonly IBucketRepository _bucketRepository;
        private readonly IValidator<BucketDto> _bucketValidator;

        public BucketService(IBucketRepository bucket, IValidator<BucketDto> bucketValidator)
        {
            _bucketRepository = bucket;
            _bucketValidator = bucketValidator ?? throw new ArgumentException();
        }

        public BucketDto CreateBucket(BucketDto bucket)
        {
            _bucketValidator.ValidateAndThrow(bucket);
            ValidateBucketUniqueness(bucket);
            return _bucketRepository.Create(bucket);
        }

        public BucketDto DeleteBucket(string bucketId)
        {
            GetBucket(bucketId);
            return _bucketRepository.Delete(Convert.ToInt32(bucketId));
        }

        public BucketDto GetBucket(string bucketId)
        {
            ValidateBucketId(bucketId);
            BucketDto bucket = _bucketRepository.GetById(Convert.ToInt32(bucketId));
            if (bucket == null)
            {
                throw new ArgumentException($"Bucket with ID {bucketId} not found");
            }
            return bucket;
        }

        public IList<BucketDto> GetBuckets()
        {
            return _bucketRepository.GetAll();
        }

        public BucketDto UpdateBucket(string bucketId, BucketDto bucket)
        {
            GetBucket(bucketId);
            _bucketValidator.ValidateAndThrow(bucket);
            return _bucketRepository.Update(Convert.ToInt32(bucketId), bucket);
        }

        public void ValidateBucketId(string bucketId)
        {
            if (string.IsNullOrEmpty(bucketId))
            {
                throw new Exception("Bucket_Error: Bucket ID is missing");
            };

            if (string.IsNullOrWhiteSpace(bucketId))
            {
                throw new Exception("Bucket_Error: Bucket ID is missing");
            };

            if (!(Regex.IsMatch(bucketId, @"^[0-9]+$", RegexOptions.Singleline)))
            {
                throw new Exception("Bucket_Error: Bucket ID should be a number");
            }
        }

        public void ValidateBucketUniqueness(BucketDto bucket)
        {
            var titleAlreadyTaken = Database.BucketDbList.Any(bct => bct.Title == bucket.Title);
            if (titleAlreadyTaken)
            {
                throw new Exception("Bucket_Error: Bucket Name should be unique");
            }
        }
    }
}
