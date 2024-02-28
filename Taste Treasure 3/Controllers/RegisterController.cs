using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Taste_Treasure_3.Models;

namespace Taste_Treasure_3.Controllers
{
    public class RegisterController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Taste Treasure;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Register register)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [User] (Email, Password, Name) VALUES (@Email, @Password, @Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", register.Email);
                command.Parameters.AddWithValue("@Password", register.Password);
                command.Parameters.AddWithValue("@Name", register.Name);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
