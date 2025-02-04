using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos;
using FluentValidation;

namespace _1_BusinessLayer.Concrete.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.email).EmailAddress().WithMessage("Please enter valid password");   
            RuleFor(x => x.password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.confirmCode).NotEmpty().WithMessage("Confirm code ise required");
            RuleFor(x => x.confirmPassword).Equal(x => x.password).WithMessage("Password does not match");
        }
    }
}
