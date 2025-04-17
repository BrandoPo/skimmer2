using Microsoft.AspNetCore.Mvc;
using skimmer2.Models;
using skimmer2.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace skimmer2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CETSNContext _context;

        public HomeController(ILogger<HomeController> logger, CETSNContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(); // Pass the users list to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccountInformation()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult CreateAcc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcc([Bind("Username,Email,Password,FirstName,LastName,Address,Role")] account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the username or email already exists
                    var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.username == account.username || a.email == account.email);
                    if (existingAccount != null)
                    {
                        ModelState.AddModelError(string.Empty, "Username or Email already exists.");
                        return View(account);
                    }

                    // Add the new account to the database
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(account);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while creating a new account.");

                // Add the error to ModelState
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");

                // Return the view with the error message
                return View(account);
            }
        }



        public IActionResult Signin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}