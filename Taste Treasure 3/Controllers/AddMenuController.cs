using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Data.SqlClient;
using Taste_Treasure_3.Models;

namespace Taste_Treasure_3.Controllers
{
    public class AddMenuController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Taste Treasure;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // This method will render the AddMenu view
        public IActionResult Index()
        {
            return View();
        }

        // This method will handle the form submission to add a new recipe
        // This method will handle the form submission to add a new recipe
        [HttpPost]
        public IActionResult AddRecipe(Recipe recipe, IFormFile photo)
        {
            try
            {
                // Check ModelState Errors
                if (!ModelState.IsValid)
                {
                    // Log ModelState errors
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        foreach (var error in ModelState[modelStateKey].Errors)
                        {
                            Console.WriteLine($"{modelStateKey}: {error.ErrorMessage}");
                        }
                    }

                    TempData["ErrorMessage"] = "Model state is not valid.";
                    return RedirectToAction("Index");
                }

                // Check if photo is provided and process it
                if (photo != null && photo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.CopyTo(memoryStream);
                        recipe.Photo = memoryStream.ToArray();
                    }
                }

                // Log recipe details before attempting to add to the database
                LogRecipeDetails(recipe);

                // Assume we have a method to save the recipe to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Here you should write your SQL query to insert the recipe into the database
                    string query = "INSERT INTO Recipe (Photo, Title, Ingredients, CategoryId) VALUES (@Photo, @Title, @Ingredients, @CategoryId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Photo", recipe.Photo);
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@CategoryId", recipe.CategoryId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Notify the user that the recipe was added successfully
                        TempData["SuccessMessage"] = "Recipe added successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Recipe could not be added. No rows affected.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                TempData["ErrorMessage"] = $"An error occurred while adding the recipe: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // Helper method to log recipe details
        private void LogRecipeDetails(Recipe recipe)
        {
            // Log recipe details
            string logMessage = $"Recipe Details - Title: {recipe.Title}, Ingredients: {recipe.Ingredients}, CategoryId: {recipe.CategoryId}";
            // Replace this with your preferred logging mechanism, such as logging to a file or database
            Console.WriteLine(logMessage);
        }

    }
}
