using System;
using System.ComponentModel.DataAnnotations;

namespace _1_BusinessLayer.Concrete.Dtos.EntryDtos
{
    public class CreateEntryDto
    {
        [Required(ErrorMessage = "PostId is required.")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Context is required.")]
        [MaxLength(1000, ErrorMessage = "Context cannot exceed 1000 characters.")]
        public string Context { get; set; }

        [Required(ErrorMessage = "DateTime is required.")]
        public DateTime DateTime { get; set; }
    }
}
