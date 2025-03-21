using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserCreateProfileDto
    {
        [Required(ErrorMessage = "The 'ProfileName' field is required.")]
        [MaxLength(50, ErrorMessage = "The 'ProfileName' cannot exceed 50 characters.")]
        public string ProfileName { get; set; }

        [Url(ErrorMessage = "The 'ImageUrl' must be a valid URL.")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The 'City' cannot exceed 50 characters.")]
        public string City { get; set; }
    }
}
