using MVC_Shop.Models;

namespace MVC_Shop.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<ProductModel> Products { get; set; } = [];
        public IEnumerable<CategoryModel> Categories { get; set; } = [];
        public int? CategoryId { get; set; }
        public PaginationVM Pagination { get; set; } = new PaginationVM();
    }
}
