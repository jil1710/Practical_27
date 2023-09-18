using Core_Practical_17.Models;
using Core_Practical_17.Repository;
using Core_Practical_17.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core_Practical_17.Controllers
{

    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _user;

        public HomeController(ILogger<HomeController> logger, IUserRepository user)
        {
            _logger = logger;
            _user = user;
        }

        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Students");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Students");   
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                if (await _user.IsUserExist(login.Email))
                {

                    if(await _user.CheckUserPassword(login.Email, login.Password))
                    {
                        var claims = new List<Claim>() {
                            new Claim(ClaimTypes.Email, login.Email),
                        };

                        foreach (var item in await _user.GetRoleByEmail(login.Email))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Students");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Login Attemp...");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attemp...");
                }
            }
            return View(login);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var user1 = await _user.IsUserExist(user.Email);
                if (!user1)
                {
                    await _user.AddUser(user);
                    TempData["Message"] = "SuccessFully Register!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "User with This Email Id is Already exists.");
                }
            }
            return View(user);
        }


        public IActionResult Logout()
        {
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookie in storedCookies)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
    }
}