using Acxiom73.Data;
using Acxiom73.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Acxiom73.Controllers
{
    public class AuthController : Controller
    {
        private readonly Dbcontext context;

        public AuthController(Dbcontext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        // Login Check POST
        [HttpPost]
        public IActionResult Login(CRMUser model)
        {
            // Check Email + Password from Database
            var user = context.CRMUsers
                               .FirstOrDefault(x => x.EmailId == model.EmailId
                                                 && x.Password == model.Password);

            if (user != null)
            {
                // SUCCESS → Dashboard
                return RedirectToAction("Dasbord", "Main");
            }

            // FAIL → Error Message
            ViewBag.Error = "Invalid Email or Password!";
            return View();
        }




        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CRMUser user)
        {
            if (ModelState.IsValid)
            {
                var exists = await context.CRMUsers
                    .FirstOrDefaultAsync(x => x.EmailId == user.EmailId);

                if (exists != null)
                {
                    ViewBag.Msg = "Email already exists!";
                    return View();
                }

                context.CRMUsers.Add(user);
                await context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            // Optional: Session, Cookies आदि clear करने हों तो यहाँ कर सकते हैं
            // HttpContext.Session.Clear();
            // Response.Cookies.Delete("YourCookieName");

            return RedirectToAction("Login");
        }

        // ------------------ FORGOT PASSWORD ----------------------

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var user = context.CRMUsers.FirstOrDefault(x => x.EmailId == email);

            if (user == null)
            {
                ViewBag.Error = "Email not found!";
                return View();
            }

            // Redirect to reset password page
            return RedirectToAction("ResetPassword", new { email = email });
        }

        // ------------------ RESET PASSWORD -----------------------

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Password and Confirm Password does not match!";
                ViewBag.Email = email;
                return View();
            }

            var user = context.CRMUsers.FirstOrDefault(x => x.EmailId == email);

            if (user == null)
            {
                ViewBag.Error = "User not found!";
                return View();
            }

            // Update password
            user.Password = password;
            user.ConfirmPassword = confirmPassword;
            context.SaveChanges();

            ViewBag.Success = "Password reset successful!";

            return RedirectToAction("Login");
        }
    }
}

    










