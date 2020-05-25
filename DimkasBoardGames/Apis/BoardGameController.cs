using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DimkasBoardGames.Infrastructure;
using DimkasBoardGames.Models;
using DimkasBoardGames.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Apis
{
    [Route("api/boardGames")]
    public class BoardGameController : Controller
    {
        private IBoardGameRepository boardGamesRepository;
        private IImageRepository imageRepository;
        private ILogger logger;
        private IHostEnvironment hostEnvironment;


        public BoardGameController(IBoardGameRepository boardGamesRepo, ILoggerFactory loggerFactory,
            IHostEnvironment hostEnvironment)
        {
            boardGamesRepository = boardGamesRepo;
            logger = loggerFactory.CreateLogger(nameof(BoardGameController));
            this.hostEnvironment = hostEnvironment;
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

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateBoardGame([FromBody] BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse {Status = false, ModelState = ModelState});
            }

            try
            {
                var newBoardGame = await boardGamesRepository.InsertBoardGameAsync(boardGame);
                if (newBoardGame == null)
                {
                    return BadRequest(new ApiResponse {Status = false});
                }

                return CreatedAtRoute("GetBoardGameRoute", new {id = newBoardGame.BoardGameId},
                    new ApiResponse {Status = true, BoardGame = newBoardGame});
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse {Status = false});
            }
        }

        // [HttpPost("images/{boardGameId}", Name = "UploadImage")]
        // // [ValidateAntiForgeryToken]
        // [ProducesResponseType(typeof(ApiResponse), 201)]
        // [ProducesResponseType(typeof(ApiResponse), 400)]
        // public async Task<ActionResult> UploadBoardGameImage([FromForm] IFormFile imageFile,
        //     [FromRoute] int boardGameId)
        // {
        //     var boardGame = await boardGamesRepository.GetBoardGameByIdAsync(boardGameId);
        //
        //     var extension = Path.GetExtension(imageFile.FileName);
        //     var imageName = boardGame.Title + DateTime.UtcNow.Ticks + extension;
        //
        //     var filePath = Path.Combine("ClientApp\\src\\assets\\images", imageName);
        //
        //     if (imageFile.Length > 0)
        //     {
        //         using (var fileStream = new FileStream(filePath, FileMode.Create))
        //         {
        //             await imageFile.CopyToAsync(fileStream);
        //         }
        //     }
        //
        //     return Ok();
        // }

        [HttpPost("images/{boardGameId}", Name = "UploadImage")]
        // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UploadBoardGameImage([FromForm] Image image,
            [FromRoute] int boardGameId)
        {
            var boardGame = await boardGamesRepository.GetBoardGameByIdAsync(boardGameId);

            var extension = Path.GetExtension(image.Name);
            var imageName = boardGame.Title + DateTime.UtcNow.Ticks + extension;
            var filePath = Path.Combine("ClientApp\\src\\assets\\images", imageName);

            if (image.ImageFile.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }
            }

            await imageRepository.InsertImageAsync(image);

            return Ok();
        }
    }
}