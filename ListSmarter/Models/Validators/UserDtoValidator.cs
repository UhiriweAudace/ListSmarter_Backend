using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ListSmarter.Models.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(b => b.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(b => b.LastName).NotEmpty().WithMessage("LastName is required");
        }
    }
}
