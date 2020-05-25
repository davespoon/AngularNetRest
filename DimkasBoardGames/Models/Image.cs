using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DimkasBoardGames.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped] public IFormFile ImageFile { get; set; }
    }
}