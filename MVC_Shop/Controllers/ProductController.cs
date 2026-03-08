using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;
using MVC_Shop.Repositories;
using MVC_Shop.Services;
using MVC_Shop.ViewModels;

namespace MVC_Shop.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {

        private readonly ProductRepository _productRepository;
        private readonly ImageService _imageService;

        public ProductController(AppDbContext context, ProductRepository productRepository, ImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
        }

        public IActionResult Index()
        {

            IEnumerable<ProductModel> products = _productRepository.GetAll();
            return View(products);
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectCategoriesAsync()
        {

            List<CategoryModel> categories = await _productRepository.GetAllCategories();
            IEnumerable<SelectListItem> selectCat = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return selectCat;
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CreateProductVM
            {
                SelectedCategores = await GetSelectCategoriesAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.SelectedCategores = await GetSelectCategoriesAsync();
                return View(vm);
            }

            ProductModel pm = new ProductModel
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price ?? 0,
                Amount = vm.Amount ?? 0,
                Color = vm.Color,
                CategoryId = vm.CategoryId,
            };

            if (vm.Image != null)
            {
                pm.Image = await _imageService.SaveImageAsync(vm.Image, "");
            }

            await _productRepository.CreateAsync(pm);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _productRepository.GetByIdAsync(id);

            if (prod != null)
            {

                if (prod.Image != null)
                {
                    _imageService.DeleteImage("", prod.Image);
                }

                _productRepository.DeleteAsync(prod);

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            ProductModel product = await _productRepository.GetByIdAsync(id);

            if (product == null) { return RedirectToAction("Index"); }

            CreateProductVM pm = new CreateProductVM
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount,
                Color = product.Color,
                Id = product.Id,
            };

            return View(pm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] CreateProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Id == null) { return View(); }

            var prod = await _productRepository.GetByIdAsync(vm.Id);

            prod.Name = vm.Name;
            prod.Description = vm.Description;
            prod.Price = vm.Price ?? 0;
            prod.Amount = vm.Amount ?? 0;
            prod.Color = vm.Color;


            if (vm.Image != null)
            {
                string root = Directory.GetCurrentDirectory();
                if (prod.Image != null)
                {
                    _imageService.DeleteImage("", prod.Image);
                }

                prod.Image = await _imageService.SaveImageAsync(vm.Image,"");
            }

            await _productRepository.UpdateAsync(prod);
            return RedirectToAction("Index");
        }

    }
}
