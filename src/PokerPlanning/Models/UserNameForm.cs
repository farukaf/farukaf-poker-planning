using System.ComponentModel.DataAnnotations;

namespace PokerPlanning.Models
{
    public record UserNameForm
    {
        [Required]
        public string UserName { get; set; }
    }
}
