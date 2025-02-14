using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos;
using FluentValidation;

namespace _1_BusinessLayer.Concrete.Validators
{
    public class UserLoginValidator : AbstractValidator<UserRegisterDto>
    {
        public UserLoginValidator()
        {
            
        }
    }
}
