namespace MVC_Shop.Models
{
    public class CategoryModel : BaseModel
    {
        public required string Name { get; set; }
        public string Icon { get; set; } = "bi bi-robot";
        public List<ProductModel> Products { get; set; } = [];
    }
}
