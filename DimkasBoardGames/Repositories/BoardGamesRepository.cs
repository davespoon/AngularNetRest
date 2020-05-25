using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Repositories
{
    public class BoardGamesRepository : IBoardGameRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger logger;

        public BoardGamesRepository(AppDbContext appDbContext, ILoggerFactory loggerFactory)
        {
            this.appDbContext = appDbContext;
            this.logger = loggerFactory.CreateLogger(nameof(BoardGamesRepository));
        }


        public async Task<List<BoardGame>> GetAllBoardGamesAsync()
        {
            return await appDbContext.BoardGames
                .Include(game => game.Feedbacks)
                .Include(game => game.BoardGameGenre)
                .Include(game => game.Image)
                .ToListAsync();
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            return await appDbContext.BoardGames
                .Include(game => game.Feedbacks)
                .Include(game => game.BoardGameGenre)
                .Include(game => game.Image)
                .FirstOrDefaultAsync(game => game.BoardGameId == id);
        }


        public async Task<BoardGame> InsertBoardGameAsync(BoardGame boardGame)
        {
            appDbContext.BoardGames.Add(boardGame);
            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"Error in {nameof(InsertBoardGameAsync)}: " + e.Message);
            }

            return boardGame;
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

        public async Task<PagingResult<BoardGame>> GetBoardGamesPageAsync(int skip, int take)
        {
            var totalRecords = await appDbContext.BoardGames.CountAsync();
            var boardGames = await appDbContext.BoardGames
                .OrderBy(c => c.Title)
                .Include(c => c.Feedbacks)
                .Include(c => c.BoardGameGenre)
                .Include(game => game.Image)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new PagingResult<BoardGame>(boardGames, totalRecords);
        }
    }
}