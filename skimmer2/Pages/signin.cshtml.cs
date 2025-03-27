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

        public string ErrorMessage { get; set; }

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

            using (var connection = new SqlConnection("YourConnectionString"))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM account WHERE (username = @Identifier OR email = @Identifier) AND password = @Password", connection);
                command.Parameters.AddWithValue("@Identifier", User.Identifier);
                command.Parameters.AddWithValue("@Password", User.Password); // Ensure password is hashed

                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    _logger.LogInformation("User signed in: {Identifier}", User.Identifier);
                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = "Incorrect username or password.";
                    return Page();
                }
            }
        }
    }

    public class UserLogin
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}