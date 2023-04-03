using Moq;
using AutoMapper;
using FluentAssertions;
using ListSmarter.Models;
using ListSmarter.Services;
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
        private readonly Mock<IBucketRepository> _mockBucketRepository;

        public BucketServiceTest()
        {
            _mapper = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            _mockBucketRepository = new Mock<IBucketRepository>();
            _bucketService = new BucketService(_mockBucketRepository.Object, new BucketDtoValidator());
        }

        [Theory]
        [InlineData(" ")]
        public void CreateBucket_ShouldThrowError_WhenBucketIdIsMissing(string title)
        {
            // Arrange
            BucketDto newBucket = new BucketDto { Title = title };
            var mockBucketRepo = _mockBucketRepository.Setup(x => x.Create(newBucket)).Returns(new BucketDto { Id = 1, Title = title });

            //act
            Action BucketResult = () => _bucketService.CreateBucket(newBucket);

            //assertions
            BucketResult.Should().Throw<FluentValidation.ValidationException>().WithMessage("Validation failed: \n -- Title: Bucket Title should not be empty Severity: Error");
        }


        [Theory]
        [InlineData("Airbnb_application")]
        [InlineData("Barbacar_app")]
        public void CreateBucket_ShouldReturnCreatedBucket(string title)
        {
            // Arrange
            BucketDto newBucket = new BucketDto { Title = title };
            var mockBucketRepo = _mockBucketRepository.Setup(x => x.Create(newBucket)).Returns(new BucketDto { Id = 1, Title = title });

            //act
            var BucketResult = _mapper.Map<Bucket>(_bucketService.CreateBucket(newBucket));

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Title.Should().Be(title);
        }

        [Theory]
        [InlineData("2")]
        public void GetBucketById_ShouldReturnExistingBucketDetails(string bucketId)
        {
            var id = Convert.ToInt32(bucketId);
            var expectedResponse = _mapper.Map<BucketDto>(GetBucketsDummyData().FirstOrDefault(x => x.Id == id));
            // Arrange
            var mockBucketRepo = _mockBucketRepository.Setup(x => x.GetById(id)).Returns(expectedResponse);

            //act
            var bucketResult = _bucketService.GetBucket(bucketId);

            // assertions
            bucketResult.Should().NotBeNull();
            bucketResult.Id.Should().Be(Convert.ToInt32(bucketId));
        }

        [Fact]
        public void GetBuckets_ShouldReturnBucketsList()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<BucketDto>>(GetBucketsDummyData());
            _mockBucketRepository.Setup(x => x.GetAll()).Returns(expectedResponse);

            //act
            var BucketResult = _bucketService.GetBuckets();

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Count.Should().BeGreaterThan(1);
        }

        [Theory]
        [InlineData("430")]
        public void GetBucketById_ShouldThrowNotFoundError(string bucketId)
        {
            // Arrange
            var id = Convert.ToInt32(bucketId);
            BucketDto expectedResponse = null;
            _mockBucketRepository.Setup(x => x.GetById(id)).Returns(expectedResponse);

            //act
            Action BucketResult = () => _bucketService.GetBucket(bucketId);

            //assertions
            BucketResult.Should().Throw<ArgumentException>().WithMessage("Bucket with ID 430 not found");
        }

        [Theory]
        [InlineData(null)]
        public void GetBucketById_ShouldThrowError_WhenBucketIdIsMissing(string bucketId)
        {
            //act
            Action BucketResult = () => _bucketService.GetBucket(bucketId);

            //assertions
            BucketResult.Should().Throw<Exception>().WithMessage("Bucket_Error: Bucket ID is missing");
        }

        [Theory]
        [InlineData("xdrt_45")]
        public void GetBucketById_ShouldThrowError_WhenBucketIdIsInvalid(string BucketId)
        {
            //act
            var BucketResult = () => _bucketService.GetBucket(BucketId);

            //assertions
            BucketResult.Should().Throw<Exception>().WithMessage($"Bucket_Error: Bucket ID should be a number");
        }

        [Theory]
        [InlineData("3", "Todos Apps")]
        [InlineData("1", "Loan application")]
        public void UpdateBucket_ShouldReturnUpdatedBucket(string bucketIdString, string updatedTitle)
        {
            // Arrange
            int bucketId = Convert.ToInt32(bucketIdString);
            BucketDto updatedBucketDto = new BucketDto { Title = updatedTitle };
            BucketDto existingBucketDto = _mapper.Map<BucketDto>(GetBucketsDummyData().FirstOrDefault(x => x.Id == bucketId));
            var expectedResponseDto = new BucketDto { Id = existingBucketDto.Id, Title = updatedTitle };

            _mockBucketRepository.Setup(x => x.GetById(bucketId)).Returns(existingBucketDto);
            _mockBucketRepository.Setup(x => x.Update(bucketId, updatedBucketDto)).Returns(expectedResponseDto);


            //act
            var BucketResult = _bucketService.UpdateBucket(bucketIdString, updatedBucketDto);

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Title.Should().Be(updatedTitle);
            BucketResult.Id.Should().Be(Convert.ToInt32(bucketId));
        }

        [Theory]
        [InlineData(3, "Todo_Application")]
        [InlineData(2, "ListSmarter_Application")]
        public void DeleteBucket_ShouldReturnDeletedBucket(int bucketId, string deletedTitle)
        {
            BucketDto existingBucketDto = _mapper.Map<BucketDto>(GetBucketsDummyData().FirstOrDefault(x => x.Id == bucketId));

            _mockBucketRepository.Setup(x => x.GetById(bucketId)).Returns(existingBucketDto);
            _mockBucketRepository.Setup(x => x.Delete(bucketId)).Returns(existingBucketDto);

            //act
            var BucketResult = _bucketService.DeleteBucket(bucketId.ToString());

            //assertions
            BucketResult.Should().NotBeNull();
            BucketResult.Id.Should().Be(bucketId);
            BucketResult.Title.Should().Be(deletedTitle);
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

