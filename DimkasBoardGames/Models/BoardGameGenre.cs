using System.Collections.Generic;

namespace DimkasBoardGames.Models
{
    public class BoardGameGenre
    {
        public int BoardGameGenreId { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public IEnumerable<BoardGame> BoardGames { get; set; }
    }
}