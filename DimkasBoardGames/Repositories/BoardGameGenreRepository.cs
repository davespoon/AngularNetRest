using System.Collections.Generic;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public class BoardGameGenreRepository : IBoardGameGenreRepository
    {
        private readonly AppDbContext _appDbContext;

        public BoardGameGenreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public IEnumerable<BoardGameGenre> Genres => _appDbContext.BoardGameGenres;
    }
}