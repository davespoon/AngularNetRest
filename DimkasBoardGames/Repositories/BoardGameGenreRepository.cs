using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace DimkasBoardGames.Repositories
{
    public class BoardGameGenreRepository : IBoardGameGenreRepository
    {
        private readonly AppDbContext appDbContext;

        public BoardGameGenreRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IEnumerable<BoardGameGenre>> GetGenresAsync()
        {
            return await appDbContext.BoardGameGenres.OrderBy(genre => genre.GenreName).ToListAsync();
        }
    }
}