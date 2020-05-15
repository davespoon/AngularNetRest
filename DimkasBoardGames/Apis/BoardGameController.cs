using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using DimkasBoardGames.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Apis
{
    [Route("api/boardGame")]
    public class BoardGameController : Controller
    {
        private IBoardGameRepository boardGameRepository;
        private ILogger logger;


        public BoardGameController(IBoardGameRepository boardGameRepo, ILoggerFactory loggerFactory)
        {
            boardGameRepository = boardGameRepo;
            logger = loggerFactory.CreateLogger(nameof(BoardGameController));
        }


        [HttpGet]
        // [NoCache]
        [ProducesResponseType(typeof(List<BoardGame>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Customers()
        {
            try
            {
                var boardGames = await boardGameRepository.GetAllBoardGamesAsync();
                return Ok(boardGames);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }
    }
}