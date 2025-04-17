using Microsoft.AspNetCore.Mvc;
using skimmer2.Models;
using skimmer2.Data;
using System.Diagnostics;

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
        public async Task<IActionResult> CreateAcc([Bind("Username,Email,Password,FirstName,LastName,Address,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username or email already exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Username or Email already exists.");
                    return View(user);
                }

                // Add the new user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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