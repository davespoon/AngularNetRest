using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DimkasBoardGames.Repositories
{
    public class StatesRepository : IStatesRepository
    {
        private readonly CustomersDbContext _Context;
        private readonly ILogger _Logger;

        public StatesRepository(CustomersDbContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("StatesRepository");
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await _Context.States.OrderBy(s => s.Abbreviation).ToListAsync();
        }
    }
}
