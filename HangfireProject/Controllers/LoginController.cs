using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HangfireProject.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUserCrendentialsService _userCrendentialsService;

        public LoginController(IUserCrendentialsService userCrendentialsService)
        {
            _userCrendentialsService = userCrendentialsService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var loginModel = new LoginModel();

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Please input username or password!";
                return View(loginModel);
            }
            var dbUser = _userCrendentialsService.GetUserCrendentialsDTO(model.Username, model.Password);

            if (dbUser != null)
            {
                List<Claim> Claims = new List<Claim>
                {
                 new Claim(ClaimTypes.NameIdentifier, model.Username),
                 new Claim(ClaimTypes.Hash, model.Password)
                };

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var user = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(user, new AuthenticationProperties
                {
                    IsPersistent = false,
                    //ExpiresUtc = DateTime.Now.AddMinutes(300) // overwrite startup
                }).ConfigureAwait(false);

                return Redirect("/myhangfire");
            }


            ViewBag.message = "Authentication Failed!";
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
