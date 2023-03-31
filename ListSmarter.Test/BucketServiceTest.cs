using System;
using AutoMapper;
using FluentAssertions;
using ListSmarter.Models;
using ListSmarter.Common;
using ListSmarter.Services;
using ListSmarter.Repositories;
using ListSmarter.Services.Interfaces;
using ListSmarter.Repositories.Models;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Models.Validators;


namespace ListSmarter.Test
{
    public class BucketServiceTest
    {
        private readonly IMapper _mapper;
        private readonly IBucketService _bucketService;

        public BucketServiceTest()
        {
            _mapper = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();
            _bucketService = new BucketService(new BucketRepository(_mapper), new BucketDtoValidator());

            foreach (Bucket bucket in GetBucketsDummyData())
            {
                Database.BucketDbList.Add(bucket);
            }
        }

        [Theory]
        [InlineData(" ")]
        public void CreateBucket_ThrowError_When_BucketId_Is_Missing(string title)
        {
            BucketDto newBucket = new BucketDto { Title = title };

            //act
            Action BucketResult = () => _bucketService.CreateBucket(newBucket);

            //assertions
            BucketResult.Should().Throw<FluentValidation.ValidationException>().WithMessage("Validation failed: \n -- Title: Bucket Title should not be empty Severity: Error");
        }


        [Theory]
        [InlineData("Airbnb_application")]
        [InlineData("Barbacar_app")]
        public void CreateBucket_test(string Title)
        {
            BucketDto newBucket = new BucketDto { Title = Title };

            //act
            var BucketResult = _mapper.Map<Bucket>(_bucketService.CreateBucket(newBucket));

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Title.Should().Be(Title);
        }

        [Theory]
        [InlineData("2")]
        public void GetBucketById(string bucketId)
        {
            //act
            var bucketResult = _bucketService.GetBucket(bucketId);

            // assertions
            bucketResult.Should().NotBeNull();
            bucketResult.Id.Should().Be(Convert.ToInt32(bucketId));
        }

        [Fact]
        public void GetBuckets_list()
        {

            //act
            var BucketResult = _bucketService.GetBuckets();

            //Console.WriteLine(JsonSerializer.Serialize(BucketResult));
            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Count.Should().BeGreaterThan(1);
        }

        [Theory]
        [InlineData("430")]
        public void GetBucketById_ThrowNotFoundError(string BucketId)
        {
            //act
            Action BucketResult = () => _bucketService.GetBucket(BucketId);

            //assertions
            BucketResult.Should().Throw<ArgumentException>().WithMessage("Bucket with ID 430 not found");
        }

        [Theory]
        [InlineData(null)]
        public void GetBucketById_ThrowError_ForMissing_BucketId(string BucketId)
        {
            //act
            Action BucketResult = () => _bucketService.GetBucket(BucketId);

            //assertions
            BucketResult.Should().Throw<Exception>().WithMessage("Bucket_Error: Bucket ID is missing");
        }

        [Theory]
        [InlineData("xdrt_45")]
        public void GetBucketById_ThrowError_For_InvalidBucketId(string BucketId)
        {
            //act
            var BucketResult = () => _bucketService.GetBucket(BucketId);

            //assertions
            BucketResult.Should().Throw<Exception>().WithMessage($"Bucket_Error: Bucket ID should be a number");
        }

        [Theory]
        [InlineData("1", "Loan application")]
        [InlineData("3", "Todos Apps")]
        public void UpdateBucket_test(string BucketId, string Title)
        {
            BucketDto updatedBucket = new BucketDto { Title = Title };

            //act
            var BucketResult = _mapper.Map<Bucket>(_bucketService.UpdateBucket(BucketId, updatedBucket));

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Title.Should().Be(Title);
            BucketResult.Id.Should().Be(Convert.ToInt32(BucketId));
        }

        [Theory]
        [InlineData("2")]
        public void DeleteBucket_test(string BucketId)
        {
            //act
            var BucketResult = _mapper.Map<Bucket>(_bucketService.DeleteBucket(BucketId));

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Title.Should().Be("ListSmarter_Application");
            BucketResult.Id.Should().Be(2);
        }

        private List<Bucket> GetBucketsDummyData()
        {
            List<Bucket> BucketsData = new List<Bucket>()
            {
                new Bucket { Id = 1,Title = "Banking_Application"},
                new Bucket { Id = 2, Title = "ListSmarter_Application"},
                new Bucket { Id = 3, Title = "Todo_Application" }
            };
            return BucketsData;
        }

    }
}

