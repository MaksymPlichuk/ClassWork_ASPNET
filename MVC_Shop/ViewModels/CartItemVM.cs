using Microsoft.AspNetCore.Mvc;

namespace MVC_Shop.ViewModels
{
    public class CartItemVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
