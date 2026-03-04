using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Services;
using MVC_Shop.ViewModels;
using static System.Net.WebRequestMethods;

namespace MVC_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var cartItems = CartService.GetItems(HttpContext.Session);
            var ids = cartItems.Select(c => c.ProductId).ToList();
            List<CartProductVM> items = [];

            if (cartItems.Count > 0)
            {
                var products = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();

                for (int i = 0; i < products.Count; i++)
                {

                    var item = new CartProductVM
                    {
                        Product = products[i],
                        Count = cartItems[i].Count
                    };
                    items.Add(item);
                }
            }

            return View(items);
        }
        public async Task<IActionResult> Add(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item != null)
            {
                if (!CartService.IsInCart(HttpContext.Session, id))
                {
                    CartService.AddToCart(HttpContext.Session, id);
                }
                else { CartService.RemoveFromCart(HttpContext.Session, id); }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Increment(int prodId)
        {
            CartService.Increment(HttpContext.Session, prodId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrement(int prodId)
        {
            CartService.Decrement(HttpContext.Session, prodId);
            return RedirectToAction("Index");
        }
    }
}
