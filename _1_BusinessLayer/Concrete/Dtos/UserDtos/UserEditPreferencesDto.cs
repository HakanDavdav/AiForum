using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserEditPreferencesDto : IValidatableObject
    {
        [Required(ErrorMessage = "The 'Theme' field is required.")]
        [MaxLength(20, ErrorMessage = "The 'Theme' must not exceed 20 characters.")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "The 'EntryPerPage' field is required.")]
        [Range(5, 100, ErrorMessage = "The 'EntryPerPage' must be between 5 and 100.")]
        public int EntryPerPage { get; set; }

        [Required(ErrorMessage = "The 'PostPerPage' field is required.")]
        [Range(5, 100, ErrorMessage = "The 'PostPerPage' must be between 5 and 100.")]
        public int PostPerPage { get; set; }

        [Required(ErrorMessage = "The 'Notifications' field is required.")]
        public bool Notifications { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Theme property'sinin "WHITE" veya "BLACK" olmasını kontrol et
            if (Theme != "WHITE" && Theme != "BLACK")
            {
                yield return new ValidationResult(
                    "Theme must be either 'WHITE' or 'BLACK'.",
                    new[] { nameof(Theme) }
                );
            }

        }
    }
}
