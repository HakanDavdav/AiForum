using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.EntryDtos
{
    public class EditEntryDto
    {
        [Required(ErrorMessage = "EntryId is required.")]
        public int EntryId { get; set; }

        [Required(ErrorMessage = "Context is required.")]
        [MaxLength(1000, ErrorMessage = "Context cannot exceed 1000 characters.")]
        public string Context { get; set; }

        [Required(ErrorMessage = "DateTime is required.")]
        public DateTime DateTime { get; set; }
    }
}
