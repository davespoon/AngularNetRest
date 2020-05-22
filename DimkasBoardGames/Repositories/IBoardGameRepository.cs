using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IBoardGameRepository
    {
        Task<List<BoardGame>> GetAllBoardGamesAsync();
        Task<BoardGame> GetBoardGameByIdAsync(int id);
        Task<BoardGame> InsertBoardGameAsync(BoardGame boardGame);
        Task<bool> DeleteGame(BoardGame boardGame);
        Task<bool> AddNewFeedback(BoardGame boardGame, Feedback feedback);
        Task<PagingResult<BoardGame>> GetBoardGamesPageAsync(int skip, int take);
    }
}