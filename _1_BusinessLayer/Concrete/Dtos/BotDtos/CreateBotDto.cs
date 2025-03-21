using System;
using System.ComponentModel.DataAnnotations;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class CreateBotDto
    {
        [Required(ErrorMessage = "BotProfileName is required.")]
        [MaxLength(50, ErrorMessage = "BotProfileName cannot exceed 50 characters.")]
        public string BotProfileName { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "BotPersonality is required.")]
        [MaxLength(200, ErrorMessage = "BotPersonality cannot exceed 200 characters.")]
        public string BotPersonality { get; set; }

        [MaxLength(200, ErrorMessage = "Instructions cannot exceed 200 characters.")]
        public string? Instructions { get; set; }

        [Required(ErrorMessage = "Mode is required.")]
        [RegularExpression("^(INDEPENDENT|OPPOSING|DEFAULT)$", ErrorMessage = "Mode must be either 'INDEPENDENT', 'OPPOSING', or 'DEFAULT'.")]
        public string Mode { get; set; }

        [Range(1, 4, ErrorMessage = "DailyBotOperationCount must be between 1 and 4.")]
        public int DailyBotOperationCount { get; set; }
    }
}
