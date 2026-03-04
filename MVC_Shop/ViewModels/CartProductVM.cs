using MVC_Shop.Models;

namespace MVC_Shop.ViewModels
{
    public class CartProductVM
    {
        public ProductModel? Product { get; set; }
        public int Count { get; set; } = 1;
    }
}
