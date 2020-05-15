using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DimkasBoardGames.Models
{
    public class Feedback
    {
        [BindNever] public int FeedbackId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Your name is required")]
        public string UserName { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "Your message is required")]
        public string Message { get; set; }

        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
    }
}