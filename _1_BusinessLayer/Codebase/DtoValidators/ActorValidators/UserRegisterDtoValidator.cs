using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ActorValidators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator(IOptions<MyConfig> cfg)
        {
            var c = cfg.Value;

            // ✅ Username Rules
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required.");


            // ✅ Email Rules
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.");

            // ✅ Password Rules
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.");

        }
    }
}
