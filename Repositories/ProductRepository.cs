using LoginRegisterIdentity.Data;
using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterIdentity.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context)
        {
			this._context = context;
		}
        public bool Add(Product product)
		{
			_context.Add(product);
			return Save();
		}

		public bool Delete(Product product)
		{
			_context.Remove(product);
			return Save();
		}

		public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetUsersProductsAsync(string AppUserId)
        {
            return await _context.Products.Where(p => p.AppUserId == AppUserId).ToListAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
		{
			return _context.Products.FirstOrDefaultAsync(x => x.Id == id);
		}

        

        public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0;
		}

		public bool Update(Product product)
		{
			_context.Update(product);
			return Save();
		}
	}
}
