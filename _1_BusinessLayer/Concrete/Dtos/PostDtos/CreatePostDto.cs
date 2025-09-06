using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "NotificationTitle is required.")]
        [MaxLength(50, ErrorMessage = "NotificationTitle cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "NotificationContext is required.")]
        [MaxLength(1000, ErrorMessage = "NotificationContext cannot exceed 1000 characters.")]
        public string Context { get; set; }

        [Required(ErrorMessage = "DateTime is required.")]
        public DateTime DateTime { get; set; }
    }
}
