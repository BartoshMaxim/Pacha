using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pacha.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pacha.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var loginRegister = new LoginRegistrationModel();
            loginRegister.RegisterModel = model;
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, SecondName = model.SecondName };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View("Login", loginRegister);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var loginRegister = new LoginRegistrationModel();
            loginRegister.LoginModel = model;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                    ModelState.AddModelError("", "Неверный логин или пароль");
                else
                {
                    if (loginRegister.LoginModel.RememberMe)
                    {
                        // Clear any other tickets that are already in the response
                        Response.Cookies.Clear();

                        // Set the new expiry date - to thirty days from now
                        DateTime expiryDate = DateTime.Now.AddDays(30);

                        // Create a new forms auth ticket
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, model.Email, DateTime.Now, expiryDate, true, String.Empty);

                        // Encrypt the ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        // Create a new authentication cookie - and set its expiration date
                        HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        authenticationCookie.Expires = ticket.Expiration;

                        // Add the cookie to the response.
                        Response.Cookies.Add(authenticationCookie);
                    }
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);

                    
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login", loginRegister);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = this.UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}