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
        private IBoardGameRepository boardGamesRepository;
        private ILogger logger;


        public BoardGameController(IBoardGameRepository boardGamesRepo, ILoggerFactory loggerFactory)
        {
            boardGamesRepository = boardGamesRepo;
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
                var boardGames = await boardGamesRepository.GetAllBoardGamesAsync();
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
                var boardGame = await boardGamesRepository.GetBoardGameByIdAsync(id);
                return Ok(boardGame);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }

        // GET api/boardGames/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<BoardGame>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BoardGamesPage(int skip, int take)
        {
            try
            {
                var pagingResult = await boardGamesRepository.GetBoardGamesPageAsync(skip, take);
                Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
                return Ok(pagingResult.Records);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }
    }
}