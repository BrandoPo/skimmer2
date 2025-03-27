using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace skimmer2.Pages
{
    public class SignInModel : PageModel
    {
        private readonly ILogger<SignInModel> _logger;

        [BindProperty]
        public UserLogin User { get; set; }

        public SignInModel(ILogger<SignInModel> logger)
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

            // Example: Check user credentials against the database
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM account WHERE username = @Username AND password = @Password", connection);
                command.Parameters.AddWithValue("@Username", User.Username);
                command.Parameters.AddWithValue("@Password", User.Password); // Ensure password is hashed

                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    _logger.LogInformation("User signed in: {Username}", User.Username);
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
        }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}