using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;
using MVC_Shop.Repositories;
using MVC_Shop.ViewModels;

namespace MVC_Shop.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {

        private readonly CategoryRepository _categoryRepository;

        public CategoryController(AppDbContext context, CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> categories = _categoryRepository.GetAll();
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

            await _categoryRepository.CreateAsync(category);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var cat = await _categoryRepository.GetByIdAsync(id);
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

            var cat = await _categoryRepository.GetByIdAsync(vm.Id);

            if (cat != null)
            {
                cat.Name = vm.Name;
                await _categoryRepository.UpdateAsync(cat);
            }
            return RedirectToAction("Index");
        }
    }
}
