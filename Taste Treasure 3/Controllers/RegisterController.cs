using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Taste_Treasure_3.Models;

namespace Taste_Treasure_3.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IMongoCollection<User> _userCollection;

        public RegisterController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("tastetreasure");
            _userCollection = database.GetCollection<User>("User");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            _userCollection.InsertOne(user);
            return RedirectToAction("Index", "Login");
        }
    }
}
