using Acxiom73.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Acxiom73.Controllers
{
    public class MainController : Controller
    {
        private readonly Dbcontext _context;

        public MainController(Dbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dasbord()
        {
            // Total Customers
            var totalCustomers = await _context.Customers.CountAsync();

            ViewBag.TotalCustomers = totalCustomers;
           

            return View();
        }

       
    }
}
