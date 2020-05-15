using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DimkasBoardGames.Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Game Title is required")]
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        [StringLength(5000)] public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageThumbnailUri { get; set; }
        public string ImageFullUri { get; set; }

        public List<Feedback> Feedbacks { get; set; }
        public virtual BoardGameGenre BoardGameGenre { get; set; }
    }
}