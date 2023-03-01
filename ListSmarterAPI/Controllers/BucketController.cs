using ListSmarter.Models;
using ListSmarter.Services.Interfaces;
using ListSmarterAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListSmarterAPI.Controllers
{
    [Route("api/buckets")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private IBucketService _bucketService;

        public BucketController(IBucketService bucketService) => _bucketService = bucketService;


        [HttpGet(Name = "GetAllBuckets")]
        public async Task<ActionResult<List<BucketDto>>> GetAll()
        {
            return await Task.FromResult(Ok(_bucketService.GetBuckets().ToList()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BucketDto>> GetOne([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_bucketService.GetBucket(id)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost(Name = "CreateBucket")]
        public async Task<ActionResult<BucketDto>> Create([FromBody] BucketModel bucketToUpdate)
        {
            try
            {
                BucketDto bucket = new BucketDto() { Title = bucketToUpdate.Title };
                return await Task.FromResult(Ok(_bucketService.CreateBucket(bucket)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BucketDto>> Update([FromRoute] string id, [FromBody] BucketModel bucketToUpdate)
        {
            try
            {
                BucketDto bucket = new BucketDto() { Title = bucketToUpdate.Title };
                return await Task.FromResult(Ok(_bucketService.UpdateBucket(id, bucket)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BucketDto>> Delete([FromRoute] string id)
        {
            try
            {
                return await Task.FromResult(Ok(_bucketService.DeleteBucket(id)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
