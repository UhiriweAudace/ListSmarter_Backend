using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Models.Validators
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Title should not be empty");
            RuleFor(b => b.Description).NotEmpty().WithMessage("Description should not be empty");
        }
    }
}
