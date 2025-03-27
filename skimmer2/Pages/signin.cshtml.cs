using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace YourNamespace.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Username or Email is required.")]
        public string Identifier { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // This property will display an error message if sign in fails.
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Any initialization code can go here.
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Replace with your actual database credentials.
            string connectionString = "server=18.119.104.251;port=3306;database=CETSN;uid=your_db_username;pwd=your_db_password;";
            
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Using a parameterized query to prevent SQL injection.
                    string sql = @"SELECT id 
                                   FROM account 
                                   WHERE (username = @identifier OR email = @identifier) 
                                     AND password = @password 
                                   LIMIT 1";
                    
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@identifier", Identifier);
                        command.Parameters.AddWithValue("@password", Password);
                        
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            // Login successful.
                            // TODO: Set authentication cookie/session here.
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            // Login failed.
                            ErrorMessage = "Incorrect username/email or password.";
                            return Page();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                ErrorMessage = "An error occurred while connecting to the database.";
                return Page();
            }
        }
    }
}
