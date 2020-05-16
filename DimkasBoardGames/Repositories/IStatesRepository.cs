using System.Collections.Generic;
using System.Threading.Tasks;
using DimkasBoardGames.Models;

namespace DimkasBoardGames.Repositories
{
    public interface IStatesRepository
    {
        Task<List<State>> GetStatesAsync();
    }
}