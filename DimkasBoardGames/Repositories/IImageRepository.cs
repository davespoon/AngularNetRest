using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetImagesAsync();
        public Task<Image> InsertImageAsync(Image image);
    }
}