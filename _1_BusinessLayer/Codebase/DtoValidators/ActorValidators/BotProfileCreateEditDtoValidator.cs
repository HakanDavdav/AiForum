using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ActorValidators
{
    public class BotProfileCreateEditDtoValidator : AbstractValidator<BotProfileCreateEditDto>
    {
        public BotProfileCreateEditDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;
            RuleFor(x => x.ProfileName)
                .NotEmpty().WithMessage("Profile name is required.")
                .MaximumLength(c.MaxProfileNameLength).WithMessage($"Profile name must be at most {c.MaxProfileNameLength} characters.");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required when AutoBio is false.")
                .MaximumLength(c.MaxBioLength).WithMessage($"Bio must be at most {c.MaxBioLength} characters.")
                .When(x => !x.AutoBio);

            RuleFor(x => x.ImageUrl)
                .MaximumLength(c.MaxImageUrlLength).WithMessage($"ImageUrl must be at most {c.MaxImageUrlLength} characters.");

            RuleFor(x => x.BotPersonality)
                .NotEmpty().WithMessage("Personality is required.")
                .MaximumLength(c.MaxPersonalityLength).WithMessage($"Personality must be at most {c.MaxPersonalityLength} characters.");

            RuleFor(x => x.Instructions)
                .NotEmpty().WithMessage("Instructions are required.")
                .MaximumLength(c.MaxInstructionLength).WithMessage($"Instructions must be at most {c.MaxInstructionLength} characters.");

            RuleFor(x => x.DailyBotOperationCount)
                .GreaterThanOrEqualTo(0).WithMessage("DailyBotOperationCount must be >= 0.")
                .LessThanOrEqualTo(c.MaxDailyBotOperationCount).WithMessage($"DailyBotOperationCount must be <= {c.MaxDailyBotOperationCount}.");
        }
    }
}
