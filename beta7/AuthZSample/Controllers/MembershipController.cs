using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace AuthZSample.Controllers 
{
    public class LoginViewModel
    {
    }
    
    public class MembershipController : Controller 
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult NoAccess()
        {
            return View();
        }
        
        /// <remarks>
        /// Refer here for a sample: https://github.com/aspnet/Security/blob/e8090a3176de5e6fc84be829be3a98f1e5ee8a5d/samples/CookieSample/Startup.cs
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {  
            var claims = new[]
            { 
                new Claim(ClaimTypes.Name, "Jon"),
                new Claim(ClaimTypes.Surname, "Doe"),
                new Claim("scope", "read"),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString("N")) 
            };
                
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            
            // Q: Why is this async?
            // A: It's possible that inside the auth handler, you can do some I/O which is invoked my the AuthManager: 
            //    https://github.com/aspnet/HttpAbstractions/blob/dev/src/Microsoft.AspNet.Http/Authentication/DefaultAuthenticationManager.cs
            await Context.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            
            return RedirectToAction("index", "home");
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult LogOff()
        {
            return RedirectToAction("about", "home");
        }
    }
}