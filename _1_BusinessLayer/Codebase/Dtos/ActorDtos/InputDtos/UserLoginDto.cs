using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username, Email, or PhoneNumber is required.")]
        [MaxLength(100, ErrorMessage = "The 'UsernameOrEmailOrPhoneNumber' cannot exceed 100 characters.")]
        public string UsernameOrEmailOrPhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }
    }
}
