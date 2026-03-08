using Microsoft.EntityFrameworkCore;
using MVC_Shop.Data;
using MVC_Shop.Models;

namespace MVC_Shop.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryModel>
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<Task> DeleteAsync(CategoryModel model)
        {
            List<ProductModel> unassignedProds = _context.Products.Where(c => c.CategoryId == model.Id).ToList();

            var res = _context.Categories.Where(c => c.Name == "Unassigned").Any();
            if (res == false)
            {
                await _context.Categories.AddAsync(new CategoryModel { Name = "Unassigned" });
                await _context.SaveChangesAsync();
            }

            if (unassignedProds.Any())
            {
                var unCat = _context.Categories.Where(c => c.Name == "Unassigned").FirstOrDefault();
                foreach (var prod in unassignedProds)
                {
                    prod.CategoryId = unCat.Id;
                }
            }
            return base.DeleteAsync(model);
        }
        public IEnumerable<CategoryModel> GetAll()
        {
            return _context.Categories.Include(c => c.Products).AsEnumerable();
        }
    }
}
