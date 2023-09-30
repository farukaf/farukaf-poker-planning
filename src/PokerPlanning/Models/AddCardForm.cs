using System.ComponentModel.DataAnnotations;

namespace PokerPlanning.Models
{
    public record AddCardForm
    {
        [Required]
        [StringLength(3, ErrorMessage = "Too long (3 character limit).")]
        public string CardValue { get; set; } = string.Empty;
    }
}
