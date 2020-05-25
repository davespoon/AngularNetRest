using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger logger;

        public async Task<IEnumerable<Image>> GetImagesAsync()
        {
            return await appDbContext.Images.ToListAsync();
        }

        public async Task<Image> InsertImageAsync(Image image)
        {
            appDbContext.Add(image);
            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"Error in {nameof(InsertImageAsync)}: " + e.Message);
            }

            return image;
        }
    }
}