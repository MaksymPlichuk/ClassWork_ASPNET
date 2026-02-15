using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;

namespace MVC_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() { 
            
            IEnumerable<ProductModel> products = _context.Products.AsEnumerable();
            return View(products);
        }
        public IActionResult Categories() {

            IEnumerable<CategoryModel> categories = _context.Categories.Include(c=>c.Products).AsEnumerable();
            return View(categories);
        }

    }
}
