using Microsoft.AspNetCore.Mvc;

namespace Acxiom73.Controllers
{
    public class SettingController : Controller
    {
        // GET: /Settings
        public IActionResult Setting()
        {
            // Agar aap database se settings laana chahte hain to yahan se load karenge
            // Filhal dummy data
            ViewBag.SiteName = "My CRM Dashboard";
            ViewBag.AdminEmail = "admin@crm.com";

            return View();
        }

        // POST: /Settings
        [HttpPost]
        public IActionResult Setting(string siteName, string adminEmail)
        {
            // Yahan aap data ko database me save kar sakte hain
            // Filhal sirf ViewBag update kar rahe hain
            ViewBag.SiteName = siteName;
            ViewBag.AdminEmail = adminEmail;

            ViewBag.Message = "Setting updated successfully!";
            return View();
        }
    }
}
