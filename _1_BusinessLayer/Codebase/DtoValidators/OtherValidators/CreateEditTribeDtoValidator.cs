using _1_BusinessLayer.Codebase.Dtos.TribeDtos.Input;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.OtherValidators
{
    public class CreateEditTribeDtoValidator : AbstractValidator<CreateEditTribeDto>
    {
        public CreateEditTribeDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;
            RuleFor(x => x.TribeName)
                .NotEmpty().WithMessage("InitiatingTribe name is required.")
                .MaximumLength(c.MaxTribeNameLength).WithMessage($"InitiatingTribe name must be at most {c.MaxTribeNameLength} characters.");

            RuleFor(x => x.Mission)
                .NotEmpty().WithMessage("Mission is required.")
                .MaximumLength(c.MaxMissionLength).WithMessage($"Mission must be at most {c.MaxMissionLength} characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(c.MaxImageUrlLength).WithMessage($"ImageUrl must be at most {c.MaxImageUrlLength} characters.");

            RuleFor(x => x.PersonalityModifier)
                .MaximumLength(c.MaxPersonalityModifierLength).WithMessage($"PersonalityModifier must be at most {c.MaxPersonalityModifierLength} characters.");

            RuleFor(x => x.InstructionModifier)
                .MaximumLength(c.MaxInstructionModifierLength).WithMessage($"InstructionModifier must be at most {c.MaxInstructionModifierLength} characters.");
        }
    }
}
