using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DimkasBoardGames.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public BoardGame BoardGame { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
