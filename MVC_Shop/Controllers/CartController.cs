using Microsoft.AspNetCore.Mvc;
using MVC_Shop.Data;
using MVC_Shop.Services;

namespace MVC_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Add(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item != null) {
                CartService.AddToCart(HttpContext.Session,id);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
