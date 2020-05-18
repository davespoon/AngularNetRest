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
    [Route("api/boardGameGenres")]
    public class BoardGameGenreController : Controller
    {
        private IBoardGameGenreRepository boardGameGenreRepository;
        private ILogger logger;


        public BoardGameGenreController(IBoardGameGenreRepository boardGameGenreRepository,
            ILoggerFactory loggerFactory)
        {
            this.boardGameGenreRepository = boardGameGenreRepository;
            this.logger = loggerFactory.CreateLogger(nameof(BoardGameGenreController));
        }


        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<BoardGameGenre>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BoardGameGenres()
        {
            try
            {
                var genres = await boardGameGenreRepository.GetGenresAsync();
                return Ok(genres);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }
    }
}