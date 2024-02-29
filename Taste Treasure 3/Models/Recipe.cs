using Microsoft.AspNetCore.Http;

namespace Taste_Treasure_3.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public int CategoryId { get; set; }

        // Add a property for file upload
        public IFormFile PhotoFile { get; set; }
    }
}
