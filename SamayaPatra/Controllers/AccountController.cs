using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SamayaPatra.Business;
using SamayaPatra.Common;
using SamayaPatra.Helper;

namespace SamayaPatra.Controllers
{
    public class AccountController : Controller
    {
        BALUser bALUser = new BALUser();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            user.Password = user.Password.ToSHA512();
            var profile = await bALUser.AuthenticateUserAsync(user);
            if (profile == null || profile.Rows.Count == 0)
            {
                ViewBag.ErrorMesssage = "Username or Password donot match";
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, profile.Rows[0]["UserID"].ToText()));
            identity.AddClaim(new Claim("UserName", profile.Rows[0]["UserName"].ToText()));
            identity.AddClaim(new Claim(ClaimTypes.Name, profile.Rows[0]["FullName"].ToText()));
            identity.AddClaim(new Claim("UserRoleID", profile.Rows[0]["UserRoleID"].ToText()));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, profile.Rows[0]["Contact"].ToText()));
            identity.AddClaim(new Claim(ClaimTypes.Email, profile.Rows[0]["Email"].ToText()));
            identity.AddClaim(new Claim(ClaimTypes.StreetAddress, profile.Rows[0]["Address"].ToText()));
            identity.AddClaim(new Claim("UserRole", profile.Rows[0]["UserRole"].ToText()));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}