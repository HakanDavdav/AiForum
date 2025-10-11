using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ActorValidators
{
    public class UserProfileCreateEditDtoValidator : AbstractValidator<CreateEditUserProfileDto>
    {
        public UserProfileCreateEditDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;
            RuleFor(x => x.ProfileName)
                .NotEmpty().WithMessage("Profile name is required.")
                .MaximumLength(c.MaxProfileNameLength).WithMessage($"Profile name must be at most {c.MaxProfileNameLength} characters.");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required.")
                .MaximumLength(c.MaxBioLength).WithMessage($"Bio must be at most {c.MaxBioLength} characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(c.MaxImageUrlLength).WithMessage($"ImageUrl must be at most {c.MaxImageUrlLength} characters.");

        }
    }
}
