using Microsoft.AspNetCore.Mvc;
using Acxiom73.Models;
using Acxiom73.Data;
using Microsoft.EntityFrameworkCore;

namespace Acxiom73.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Dbcontext context;

        public object FullName { get; private set; }

        public CustomerController(Dbcontext context)
        {
            this.context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await context.Customers.ToListAsync());
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Add(customer);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                context.Update(customer);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: Customer/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX ke liye search
        [HttpGet]
        public IActionResult SearchJson(string searchTerm)
        {
            var customers = context.Customers
                .Where(c => !string.IsNullOrEmpty(searchTerm) &&
                            c.FullName.Contains(searchTerm))
                .Select(c => new {
                    fullName = c.FullName,
                    email = c.Email,
                    phone = c.Phone,
                    address = c.Address,
                    status = c.Status
                })
                .ToList();

            return Json(customers);
        }

    }

}

