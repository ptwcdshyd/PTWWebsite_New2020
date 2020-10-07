using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginController(IUserService UserService, IHttpContextAccessor contextAccessor)
        {
            _UserService = UserService;
            _contextAccessor = contextAccessor;
        }
        [Route("Login")]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(
           scheme: "PTWSecurityScheme");
            return View();
        }

        [HttpPost]
        public IActionResult Login_Post([FromBody] LoginUser model)
        {
            try
            {
                DataTable result = _UserService.Authenticate(model);

                if (result != null && result.Rows.Count > 0)
                {
                    string Username = Convert.ToString(result.Rows[0]["Username"]);

                    //Insert session in all cases either authenticated or not 
                    string sessionId = _contextAccessor.HttpContext.Session.Id;
                    _UserService.InsertSession(sessionId, Username);
                    _UserService.InsertUserlog(Username, sessionId);

                    var claims = new List<Claim>
                            {
                         new Claim(ClaimTypes.NameIdentifier,Username),

                         new Claim("RoleName", Convert.ToString(result.Rows[0]["RoleName"])),
                         new Claim("SessionId",sessionId),
                          new Claim(ClaimTypes.Webpage,"")

                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync("PTWSecurityScheme", principal, new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,

                    });
                }
         
                return Json(new { Message = result }, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("Logout")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Logout()
        {
            if (User.FindFirst("SessionId") != null)
            {
                string sessionId = User.FindFirst("SessionId").Value;

                _UserService.InsertUserlog(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString(), sessionId);
            }

            await HttpContext.SignOutAsync(
                    scheme: "PTWSecurityScheme");


            return RedirectToAction("Login", "Login");
        }
    }
}
