namespace Taste_Treasure_3.Models
{
    public class Register
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public byte[] RecipeImage { get; set; } 
    }
}
