using Microsoft.AspNetCore.Mvc;
using MVC_Shop.Data;
using MVC_Shop.Models;
using MVC_Shop.ViewModels;
using System.Diagnostics;

namespace MVC_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? c, [FromQuery] PaginationVM pagination)
        {
            List<CategoryModel> categories = _context.Categories.ToList();
            IQueryable<ProductModel> products = _context.Products;

            if (c != null && categories.Any(cp => cp.Id == c))
            {
                products = products.Where(p => p.CategoryId == c);
            }

            pagination.PageSize = pagination.PageSize < 0 ? 20 : pagination.PageSize;
            pagination.PageCount = (int)Math.Ceiling((double)products.Count() / pagination.PageSize);
            pagination.Page = pagination.Page < 0 || pagination.Page > pagination.PageCount ? 1 : pagination.Page;

            products = products.OrderBy(p => p.Id)
                .Skip(pagination.PageSize * (pagination.Page - 1)).Take(pagination.PageSize);

            var homeVM = new HomeVM
            {
                Products = products.AsEnumerable(),
                Categories = categories.AsEnumerable(),
                Pagination = pagination,
                CategoryId = c
            };

            return View(homeVM);
        }

        public async Task<IActionResult> Description(int? id)
        {
            List<CategoryModel> categories = _context.Categories.ToList();
            var product = await _context.Products.FindAsync(id);

            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
