using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel.DataAnnotations;

namespace MVC_Shop.ViewModels
{
    public class CreateProductVM
    {
        [Required (ErrorMessage = "Field is Required")]
        [MinLength(3, ErrorMessage = "Minimal length 3")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Field is Required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Field is Required")]
        [Range(1,9999999, ErrorMessage = "Price range is from 1-9999999")]
        public double? Price { get; set; } = 0d;
        [Required(ErrorMessage = "Field is Required")]
        [Range(0, 9999999, ErrorMessage = "Price ammoumt is from 0-9999999")]
        public int? Amount { get; set; } = 0;
        [Required(ErrorMessage = "Field is Required")]
        [MinLength(3, ErrorMessage = "Minimal length 3")]
        public string? Color { get; set; }
        public IFormFile? Image { get; set; }

        public int? Id { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> SelectedCategores { get; set; } = [];
    }
}
