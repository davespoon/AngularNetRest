﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IBoardGameRepository
    {
        Task<List<BoardGame>> GetAllBoardGamesAsync();
        Task<BoardGame> GetBoardGameByIdAsync(int id);
        Task<bool> AddNewGame(BoardGame boardGame);
        Task<bool> DeleteGame(BoardGame boardGame);
        Task<bool> AddNewFeedback(BoardGame boardGame, Feedback feedback);
    }
}