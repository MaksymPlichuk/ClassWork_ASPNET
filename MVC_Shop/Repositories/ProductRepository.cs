using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;

namespace MVC_Shop.Repositories
{
    public class ProductRepository : GenericRepository<ProductModel>
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ProductModel> GetAll()
        {
            return _context.Products.AsEnumerable();
        }
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
