using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;

namespace _1_BusinessLayer.Codebase.DtoValidators.ActorValidators
{
    public class BotProfileCreateEditDtoValidator : AbstractValidator<CreateEditBotProfileDto>
    {
        public BotProfileCreateEditDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;

            RuleFor(x => x.ProfileName)
                .NotEmpty().WithMessage("Profile name is required.")
                .MaximumLength(c.MaxProfileNameLength).WithMessage($"Profile name must be at most {c.MaxProfileNameLength} characters.");

            // Bio rules
            RuleFor(x => x.Bio)
                .MaximumLength(c.MaxBioLength).WithMessage($"Bio must be at most {c.MaxBioLength} characters.")
                .When(x => !string.IsNullOrEmpty(x.Bio));

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required when AutoBio is false.")
                .When(x => x.AutoBio == false);

            RuleFor(x => x.ImageUrl)
                .MaximumLength(c.MaxImageUrlLength).WithMessage($"ImageUrl must be at most {c.MaxImageUrlLength} characters.");

            RuleFor(x => x.BotPersonality)
                .NotEmpty().WithMessage("Personality is required.")
                .MaximumLength(c.MaxPersonalityLength).WithMessage($"Personality must be at most {c.MaxPersonalityLength} characters.");

            RuleFor(x => x.Instructions)
                .NotEmpty().WithMessage("Instructions are required.")
                .MaximumLength(c.MaxInstructionLength).WithMessage($"Instructions must be at most {c.MaxInstructionLength} characters.");

            RuleFor(x => x.DailyBotOperationCount)
                .NotEmpty().WithMessage("DailyBotOperationCount is required.")
                .GreaterThanOrEqualTo(0).WithMessage("DailyBotOperationCount must be >= 0.")
                .LessThanOrEqualTo(c.MaxDailyBotOperationCount).WithMessage($"DailyBotOperationCount must be <= {c.MaxDailyBotOperationCount}.");

            RuleFor(x => x.BotMode)
                .NotEmpty().WithMessage("BotMode is required.")
                .Must(b => b.HasValue && Enum.IsDefined(b.Value.GetType(), b.Value))
                .WithMessage("BotMode must be a valid value.");

            RuleFor(x => x.AutoInterests)
                .NotNull().WithMessage("AutoInterests must be specified.");

            RuleFor(x => x.AutoBio)
                .NotNull().WithMessage("AutoBio must be specified.");

            When(x => x.AutoInterests == false, () =>
            {
                RuleFor(x => x.Interests)
                    .NotNull().WithMessage("Interests are required when AutoInterests is false.")
                    .Must(interests => interests.HasValue && CountSetBits(Convert.ToInt64(interests.Value)) >= 3)
                    .WithMessage("At least 3 interests must be selected when AutoInterests is false.");
            });
        }

        private static int CountSetBits(long value)
        {
            int count = 0;
            while (value != 0)
            {
                count += (int)(value & 1);
                value >>= 1;
            }
            return count;
        }
    }
}
