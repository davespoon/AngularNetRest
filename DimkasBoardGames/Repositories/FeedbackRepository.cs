using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private AppDbContext appDbContext;


        public FeedbackRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<bool> AddFeedbackAsync(Feedback feedback)
        {
            appDbContext.Feedbacks.Add(feedback);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Feedback> GetFeedbackAsync(int feetbackId)
        {
            throw new System.NotImplementedException();
        }
    }
}