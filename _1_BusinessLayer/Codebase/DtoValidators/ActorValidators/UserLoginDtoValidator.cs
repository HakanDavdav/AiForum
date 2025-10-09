using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace _1_BusinessLayer.Codebase.DtoValidators.ActorValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator(IOptions<MyConfig> cfg)
        {
            RuleFor(x => x.UsernameOrEmailOrPhoneNumber)
                .NotEmpty().WithMessage("Username, Email, or PhoneNumber is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

        }
    }
}
