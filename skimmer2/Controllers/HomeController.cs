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

        public IActionResult Signin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        //the following is for the accountcreation page
    private readonly ApplicationDbContext _acccontext;

    public HomeController(ApplicationDbContext context)
    {
        _acccontext = context;
    }

    [HttpGet]
    public IActionResult SubmitForm()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitForm([Bind("Username,Email,Password,FirstName,LastName,Address,Role")] User user)
    {
        if (ModelState.IsValid)
        {
            // Hash the password before saving
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _acccontext.Add(user);
            await _acccontext.SaveChangesAsync();
            return RedirectToAction(nameof(Success));
        }
        return View(user);
    }

    public IActionResult Success()
    {
        return View();
    }



    }
}