using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ContentItemValidators
{
    public class CreateEditPostDtoValidator : AbstractValidator<CreateEditPostDto>
    {
        public CreateEditPostDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(c.MaxPostTitleLength).WithMessage($"Title must be at most {c.MaxPostTitleLength} characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(c.MaxContentLength).WithMessage($"Content must be at most {c.MaxContentLength} characters.");
        }
    }
}
