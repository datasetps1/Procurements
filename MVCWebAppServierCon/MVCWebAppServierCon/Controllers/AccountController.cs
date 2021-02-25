using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;
using MySql.Data.MySqlClient;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

[assembly: log4net.Config.Repository()]

namespace MVCWebAppServierCon.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IStringLocalizer<AccountController> _localizer;
        private readonly SecondConnClass _sc;

        private readonly IConfiguration configuration;
        private IHostingEnvironment hostingEnviroment { get; }

        string conString = "";
        SqlConnection connection;

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;



        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, SecondConnClass sc)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _sc = sc;
            configuration = config;
            conString = configuration.GetConnectionString("Myconnection");
            connection = new SqlConnection(conString);
            this.hostingEnviroment = hostingEnviroment;
            //_localizer = localizer;
        }
        //public AccountController()
        //{

        //}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            CompanyDefinition();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel rc)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = rc.Name, Email = rc.Emial, PasswordHash = rc.Password };
                var res = await userManager.CreateAsync(user, rc.Password);
                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    ViewBag.User = signInManager.IsSignedIn(User) + " ---- " + User.Identity.Name;
                    return RedirectToAction("index", "Home");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(rc);
        }
        [HttpPost]


        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();



            // Session(); 
            return RedirectToAction("Index", "Home");

            // return RedirectToAction("Login");
        }



        //public IActionResult Login()
        //{
        //    return View();
        //}
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel lc, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var res = await signInManager.PasswordSignInAsync(lc.Email, lc.Password, lc.RememberMe, false);
                if (res.Succeeded)
                {


                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "Home");
                    }
                }

                ModelState.AddModelError(String.Empty, "Invalid Login Attempt");
            }
            return View(lc);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                //  if (user != null && await userManager.IsEmailConfirmedAsync(user))
                if (user != null)
                {
                    // Generate the reset password token
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    // Log the password reset link
                    //logger.Log(LogLevel.Warning, passwordResetLink);
                    SendEmail(passwordResetLink, model.Email);
                    // Send the user to Forgot Password Confirmation view
                   // return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        private JsonResult SendEmail(string ResetPassword, string useremail)
        {

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            var mail = new MailMessage();

            mail.From = new MailAddress("DatasetGlob@gmail.com");

            mail.To.Add(useremail);

            mail.Subject = "Test Mail - 1";

            mail.IsBodyHtml = true;

            string htmlBody;



            htmlBody = "<html><body><p> Dear </p><p> please enter the following link to reset your password </p>" + ResetPassword + "<p>Regards</p><p>Dataset Software Solutions</p></body></html>";





            mail.Body = htmlBody;

            SmtpServer.Port = 587;

            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Credentials = new System.Net.NetworkCredential("DatasetGlob@gmail.com", "Dataset@1234");

            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);



            return new JsonResult(new { success = true, data = "ok" });

        }

        public List<LoginViewModel> CompanyDefinition()
            
        {// use sql command to make new query to get data from cost table that needed in the order

            
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT street2, LogoExt, LogoPath from CompanyDefinition where flag = 1;", connection);
            var reader = command.ExecuteReader();
            List<LoginViewModel> costLst = new List<LoginViewModel>();
            reader.Read();
            
            LoginViewModel costs = new LoginViewModel();
            costs.Name = reader.GetValue(0).ToString();
            costs.LogoExt = reader.GetValue(1).ToString();
            costs.logosting = reader.GetValue(2).ToString();
            costs.LogoPath = System.Text.Encoding.UTF8.GetBytes(reader.GetValue(2).ToString());
            costs.LogoPath = (byte[])reader["LogoPath"];

          //  ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
            ViewBag.Base64String = "data:image/jpg;base64," + Convert.ToBase64String(costs.LogoPath);
            ViewBag.CompName = costs.Name;
            costLst.Add(costs);
            

                // do something with 'value'
            
            connection.Close();
            return costLst;
        }

    }
}