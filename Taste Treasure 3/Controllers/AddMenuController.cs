using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Taste_Treasure_3.Models;

namespace Taste_Treasure_3.Controllers
{
    public class AddMenuController : Controller
    {
        private readonly IMongoCollection<Recipe> _recipeCollection;

        public AddMenuController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("tastetreasure");
            _recipeCollection = database.GetCollection<Recipe>("Recipe");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Recipe());
        }

        [HttpPost]
        public IActionResult Index(Recipe recipe, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Convert image to byte array
                    byte[] imageData = null;
                    if (photo != null)
                    {
                        using (var stream = photo.OpenReadStream())
                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            imageData = memoryStream.ToArray();
                        }
                    }

                    // Create Recipe object
                    var recipeObject = new Recipe
                    {
                        Id = ObjectId.GenerateNewId().ToString(), 
                        Photo = imageData,
                        Title = recipe.Title,
                        Ingredients = recipe.Ingredients,
                        CategoryId = recipe.CategoryId
                    };

                    // Insert into MongoDB
                    _recipeCollection.InsertOne(recipeObject);

                   
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the recipe.");
                    return View(recipe);
                }
            }

            // If the model state is not valid, return to the form with validation errors
            return View(recipe);
        }

    }
}
