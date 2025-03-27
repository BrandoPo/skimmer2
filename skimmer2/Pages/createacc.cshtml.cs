using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace skimmer2.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public UserRegistration User { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update the connection string with your actual database username and password.
            string connectionString = "server=18.119.104.251;port=3306;database=CETSN;uid=your_db_username;pwd=your_db_password;";
            
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO account 
                                   (username, email, password, first_name, last_name, address, role) 
                                   VALUES (@username, @email, @password, @firstName, @lastName, @address, @role)";
                    
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", User.Username);
                        command.Parameters.AddWithValue("@email", User.Email);
                        command.Parameters.AddWithValue("@password", User.Password);
                        command.Parameters.AddWithValue("@firstName", User.FirstName);
                        command.Parameters.AddWithValue("@lastName", User.LastName);
                        command.Parameters.AddWithValue("@address", User.Address);
                        command.Parameters.AddWithValue("@role", User.Role);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("User registered: {Username}", User.Username);
                            // Optionally set a success message or redirect.
                            SuccessMessage = "Registration successful. You may now sign in.";
                            return RedirectToPage("/SignIn");
                        }
                        else
                        {
                            ErrorMessage = "An error occurred while creating your account. Please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                ErrorMessage = "An error occurred: " + ex.Message;
            }

            return Page();
        }
    }

    public class UserRegistration
    {
        [Required]
        public string Username { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Role { get; set; }
    }
}
