using Microsoft.IdentityModel.Tokens;
using MVC_Shop.Data;
using MVC_Shop.Models;

namespace MVC_Shop.Repositories
{
    public class GenericRepository<TModel>
        where TModel : class, IBaseModel
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TModel model)
        {
            await _context.Set<TModel>().AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TModel model)
        {
            _context.Set<TModel>().Update(model);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TModel model)
        {
            _context.Set<TModel>().Remove(model);
            await _context.SaveChangesAsync();
        }
        public async Task<TModel>? GetByIdAsync(int? id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }
    }
}
