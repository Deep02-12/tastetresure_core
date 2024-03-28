using Microsoft.AspNetCore.Http;

namespace Taste_Treasure_3.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public byte[] Photo { get; set; }
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public int CategoryId { get; set; }
    }

}
