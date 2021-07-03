using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShop.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [Route("/login")]
        public async Task<IActionResult> Login([FromQuery] string user, [FromQuery] string pwd)
        {
            if (user == "admin" && pwd == "password")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user),
                    new Claim(ClaimTypes.Email, "admin@eshop.com"),
                    new Claim(ClaimTypes.HomePhone, "123123123")
                };

                var userIdentity = new ClaimsIdentity(userClaims, "eShop.CookieAuth");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

               await HttpContext.SignInAsync("eShop.CookieAuth", userPrincipal);
            }

            return Redirect("/outstandingorders");
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/outstandingorders");
        }
    }
}
