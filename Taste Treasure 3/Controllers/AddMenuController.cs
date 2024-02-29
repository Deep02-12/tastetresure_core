using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Taste_Treasure_3.Models;

namespace Taste_Treasure_3.Controllers
{
    public class AddMenuController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Taste Treasure;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet]
        public IActionResult Index()
        {
            var recipe = new Recipe();
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Index(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Define the SQL query
                        string query = "INSERT INTO Recipe (Title, Ingredients, CategoryId) VALUES (@Title, @Ingredients, @CategoryId)";

                        // Create a command object
                        using (var command = new SqlCommand(query, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@Title", recipe.Title);
                            command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                            command.Parameters.AddWithValue("@CategoryId", recipe.CategoryId);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }

                    // Redirect to the home page after successfully adding the recipe
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
