using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFin.v2.Models.Entities;
using MyFin.v2.Models.services.ifaces;
using System.Security.Claims;

namespace MyFin.v2.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> _logger)
        {
            this.accountService = accountService;
            this._logger = _logger;
        }

        public ActionResult Index()
        {
            _logger.LogInformation("start");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
            var response = accountService.login(email, password).Result;
            if (response.status == 200)
            {
                await Authenticate(response.message);
            }
            return Json(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string name, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return Json(new Response { status = 403, message = "invalid input" });
            }
            var response = accountService.register(name, password, email).Result;
            if (response.status == 200)
            {
                await Authenticate(response.message);
            }
            return Json(new { status = 200 });
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            logoutDRY();
            return Redirect("/");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Json(new { status = 500, message = "invalid password" });
            }
            String idUser = User.Claims.First().Value;
            var response = accountService.removeAccount(password, idUser);
            if (response.Result.status == 200)
            {
                logoutDRY();
            }
            return Json(response.Result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            String idUser = User.Claims.First().Value;
            var response = accountService.rename(name, idUser);
            return Json(response.Result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(string old_password, string new_password)
        {
            if (string.IsNullOrWhiteSpace(new_password) || string.IsNullOrWhiteSpace(old_password))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            String idUser = User.Claims.First().Value;
            var response = accountService.changePassword(old_password, new_password, idUser).Result;
            return Json(response);
        }

        private async Task Authenticate(string idUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, idUser)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async void logoutDRY()
        {
            accountService.logout();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /*
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var response = new { status = 509, message = filterContext.Exception.Message };
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = response
                };
                filterContext.ExceptionHandled = true;
            }
        }*/

    }
}
