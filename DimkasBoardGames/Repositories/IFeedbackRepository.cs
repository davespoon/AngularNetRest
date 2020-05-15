using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IFeedbackRepository
    {
        Task<bool> AddFeedbackAsync(Feedback feedback);
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback> GetFeedbackAsync(int feetbackId);
    }
}