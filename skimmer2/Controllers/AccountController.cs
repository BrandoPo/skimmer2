using Microsoft.AspNetCore.Mvc;
using skimmer2.Models;
using skimmer2.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace skimmer2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly CETSNContext _context;

        public AccountController(ILogger<AccountController> logger, CETSNContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /Account/SignIn
        public IActionResult SignIn()
        {
            // If the user is already signed in, redirect to the account home page
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Home", "Account");
            }
            return View();
        }

        // POST: /Account/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required.");
                return View();
            }

            // Check if the user exists by username or email
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.username == username || a.email == username);

            if (user == null || user.password != password)
            {
                ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
                return View();
            }

            // Store the username in session to indicate the user is signed in
            HttpContext.Session.SetString("Username", user.username);
            return RedirectToAction("Home", "Account");
        }

        // GET: /Account/Home
        public IActionResult Home()
        {
            // Check if the user is signed in
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("SignIn");
            }
            // Fetch user details or any other necessary data
            var username = HttpContext.Session.GetString("Username");
            var user = _context.Accounts.FirstOrDefault(a => a.username == username);
            return View(user);
        }

        // GET: /Account/SignOut
        public IActionResult SignOut()
        {
            // Clear the session to sign out the user
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}