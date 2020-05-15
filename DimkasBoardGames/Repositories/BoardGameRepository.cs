using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace DimkasBoardGames.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly AppDbContext appDbContext;


        public BoardGameRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync()
        {
            return await appDbContext.BoardGames.Include(game => game.Feedbacks).Include(game => game.BoardGameGenre)
                .ToListAsync();
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            return await appDbContext.BoardGames.Include(game => game.Feedbacks)
                .FirstOrDefaultAsync(game => game.BoardGameId == id);
        }


        //todo change hardcored true
        public async Task<bool> AddNewGame(BoardGame boardGame)
        {
            appDbContext.BoardGames.Add(boardGame);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGame(BoardGame boardGame)
        {
            appDbContext.BoardGames.Remove(boardGame);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddNewFeedback(BoardGame boardGame, Feedback feedback)
        {
            appDbContext.BoardGames.FirstOrDefault(game => game.BoardGameId == boardGame.BoardGameId)?.Feedbacks
                .Add(feedback);
            return await appDbContext.SaveChangesAsync() > 0;
        }
    }
}