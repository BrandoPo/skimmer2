using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace skimmer2.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            // Optionally initialize any page state.
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Connection string to the MySQL database
            // Replace your_db_username and your_db_password with actual credentials.
            string connectionString = "server=18.119.104.251;port=3306;database=CETSN;uid=root;pwd=llatsni;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO account 
                                   (username, email, password, first_name, last_name, address, role) 
                                   VALUES (@username, @email, @password, @first_name, @last_name, @address, @role)";

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", Username);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@password", Password);
                        command.Parameters.AddWithValue("@first_name", FirstName);
                        command.Parameters.AddWithValue("@last_name", LastName);
                        command.Parameters.AddWithValue("@address", Address);
                        command.Parameters.AddWithValue("@role", Role);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Optionally, you can set a success message or redirect to a Sign In page.
                            SuccessMessage = "Registration successful. You may now sign in.";
                            // Redirect to Sign In page after successful registration:
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
                // Log the exception as needed.
                ErrorMessage = "An error occurred: " + ex.Message;
            }

            return Page();
        }
    }
}
