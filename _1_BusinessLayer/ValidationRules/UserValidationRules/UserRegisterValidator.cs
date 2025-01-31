using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Dtos;
using FluentValidation;

namespace _1_BusinessLayer.ValidationRules.UserValidationRules
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(UserRegister => UserRegister.username).NotEmpty().WithMessage("Username is empty");
            RuleFor(UserRegister => UserRegister.password).NotEmpty().WithMessage("Password is empty");
            RuleFor(UserRegister => UserRegister.email).NotEmpty().WithMessage("Password is empty");
        }
    }
}
