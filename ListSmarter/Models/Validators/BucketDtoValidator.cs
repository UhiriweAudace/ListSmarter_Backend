using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using ListSmarter.Repositories;

namespace ListSmarter.Models.Validators
{
    public class BucketDtoValidator : AbstractValidator<BucketDto>
    {
        private readonly IEnumerable<BucketDto> _buckets;
        public BucketDtoValidator(IEnumerable<BucketDto> buckets)
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Bucket Title should not be empty");
        }       
    }
}
