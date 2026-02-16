using Microsoft.Extensions.FileProviders;

namespace MVC_Shop.ViewModels
{
    public class CreateProductVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Amount { get; set; }
        public string? Color { get; set; }
        public IFormFile? Image { get; set; }
    }
}
