using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;
using MVC_Shop.ViewModels;

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

        public async Task<IActionResult> Create() { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateProductVM vm)
        {
            var categories = _context.Categories.FirstOrDefault();

            if (categories == null) {
                return View();
            }
            ProductModel pm = new ProductModel
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price ?? 0,
                Amount = vm.Amount ?? 0,
                Color = vm.Color,
                Category = categories
            };

            if (vm.Image != null) { 
                var root = Directory.GetCurrentDirectory();
                var path = Path.Combine(root, "wwwroot", "images");

                var ext = Path.GetExtension(vm.Image.FileName);
                var name = Guid.NewGuid().ToString();

                var fileName = name + ext;
                var filePath = Path.Combine(path, fileName);
            
                using var openStream = vm.Image.OpenReadStream();
                using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                openStream.CopyTo(fs);

                pm.Image = fileName;
            }
            await _context.Products.AddAsync(pm);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _context.Products.FindAsync(id);

            if (prod != null) {

                string root = Directory.GetCurrentDirectory();
                string path = Path.Combine(root, "wwwroot", "images", $"{prod.Name}");
                Console.WriteLine($"\t\t\t{path}");
            }
            
            return RedirectToAction("Index");
        }
    }
}
