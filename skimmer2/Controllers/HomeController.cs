﻿using Microsoft.AspNetCore.Mvc;
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

       /* public IActionResult CreateAcc([Bind("username,email,password,first_name,last_name,address,Role")] account model)
{
    if (ModelState.IsValid)
    {
        // Hash the password before saving
        model.password = BCrypt.Net.BCrypt.HashPassword(model.password);
        _context.Add(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Success)); // You need to create this action or redirect to another page
    }
    return View(model);
}

public IActionResult Success()
{
    return View(); // Create a Success view or redirect to another appropriate page
}*/


    }
}