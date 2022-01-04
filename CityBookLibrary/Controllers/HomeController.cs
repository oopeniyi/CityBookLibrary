using CityBookLibrary.Models;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace CityBookLibrary.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
               
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser appUser)
        {

            var user = await _userManager.FindByNameAsync(appUser.UserName);

            if (user != null)
            { 
                var signInResult = await _signInManager.PasswordSignInAsync(user, appUser.Password, false, false);

                if (signInResult.Succeeded)
                {                    
                    return RedirectToAction("Index", "Book");
                }
            }
         
            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser appUser)
        {
            
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString("N"),
                UserName = appUser.Email,
                Email = appUser.Email,
                FirstName = appUser.FirstName,                
                RegistrationDate = DateTime.Now,
                Password = appUser.Password,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, user.Password);


            if (result.Succeeded)
            {               
                var signInResult = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Book");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
