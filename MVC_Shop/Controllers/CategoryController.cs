using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;
using MVC_Shop.ViewModels;

namespace MVC_Shop.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> categories = _context.Categories.Include(c => c.Products).AsEnumerable();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }

            var category = new CategoryModel { Name = vm.Name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                List<ProductModel> unassignedProds = _context.Products.Where(c => c.CategoryId == id).ToList();

                var res = _context.Categories.Where(c => c.Name == "Unassigned").Any();
                if (res == false)
                {
                    await _context.Categories.AddAsync(new CategoryModel { Name = "Unassigned" });
                    await _context.SaveChangesAsync();
                }

                if (unassignedProds.Any())
                {
                    var unCat = _context.Categories.Where(c => c.Name == "Unassigned").FirstOrDefault();
                    foreach (var prod in unassignedProds)
                    {
                        prod.CategoryId = unCat.Id;
                    }
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            var formCat = new CreateCategoryVM { Name = cat.Name, Id = cat.Id };

            if (cat != null)
            {
                return View(formCat);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CreateCategoryVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            if (vm.Id == null) { return View(vm); }

            var cat = await _context.Categories.FindAsync(vm.Id);

            if (cat != null)
            {
                cat.Name = vm.Name;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
