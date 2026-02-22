using MVC_Shop.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Shop.ViewModels
{
    public class CreateCategoryVM
    {
        [Required(ErrorMessage ="Name is Required!")]
        public required string Name { get; set; }
        public int? Id { get; set; }
    }
}
