using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ContentItemValidators
{
    public class CreateEditEntryDtoValidator : AbstractValidator<CreateEditEntryDto>
    {
        public CreateEditEntryDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(c.MaxContentLength).WithMessage($"Content must be at most {c.MaxContentLength} characters.");

        }
    }
}
