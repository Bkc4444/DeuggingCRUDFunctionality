using CPW219_AspnetMVC_CRUD_Debugging.Data;
using CPW219_AspnetMVC_CRUD_Debugging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_AspnetMVC_CRUD_Debugging.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Product.Add(product);

                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{product.Name} was added successfully";
                return View();
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product? product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{product.Name} was updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if(product != null)
            {
                _context.Product.Remove(product);

                await _context.SaveChangesAsync();

                TempData["Message"] = product.Name + "was deleted successfully";

                return RedirectToAction("Index");

            }

            TempData["Message"] = "This song was already deleted";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductExists(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
