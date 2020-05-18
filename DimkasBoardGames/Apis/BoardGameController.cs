using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Infrastructure;
using DimkasBoardGames.Models;
using DimkasBoardGames.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Apis
{
    [Route("api/boardGames")]
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
        [NoCache]
        [ProducesResponseType(typeof(List<BoardGame>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BoardGames()
        {
            try
            {
                var boardGames = await boardGameRepository.GetAllBoardGamesAsync();
                return Ok(boardGames);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }


        [HttpGet("{id}", Name = "GetBoardGameRoute")]
        [NoCache]
        [ProducesResponseType(typeof(BoardGame), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BoardGames(int id)
        {
            try
            {
                var boardGame = await boardGameRepository.GetBoardGameByIdAsync(id);
                return Ok(boardGame);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }
    }
}