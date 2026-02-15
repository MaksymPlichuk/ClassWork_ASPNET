using MVC_Shop.Data;

namespace MVC_Shop.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
