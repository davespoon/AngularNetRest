using System.Collections.Generic;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IBoardGameGenreRepository
    {
        IEnumerable<BoardGameGenre> Genres { get; }
    }
}