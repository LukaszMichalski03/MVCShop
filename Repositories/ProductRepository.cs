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
		public bool AddImage(Image image)
		{
			_context.Add(image);
			return Save();
		}
		public bool AddToCard(ShoppingCard item)
		{
			_context.Add(item);
			return Save();
		}
		public async Task<IEnumerable<Product>> GetShoppingCardItemsByUserId(string userId)
		{
			List<Product> result = new List<Product>();
            var productsId = await _context.ShoppingCards.Where(x => x.AppUserId == userId).Select(item => item.ProductId).ToListAsync();
			foreach(var item in productsId)
			{
				var product = _context.Products.FirstOrDefault(p => p.Id == item);
				if (product != null) result.Add(product);
			}


			return result;
		}
		public bool Delete(Product product)
		{
			_context.Remove(product);
			return Save();
		}
		public bool DeleteImage(Image image)
		{
			_context.Remove(image);
			return Save();
		}
		public async Task<IEnumerable<string>> GetProductsImagesAsync(int productId)
		{
			var imageLinks = await _context.Images.Where(x => x.ProductId == productId)
				.Select(image => image.ImageLink)
				.ToListAsync();

			return imageLinks;
		}
		public bool DeleteProductsImages(int productId)
		{
			var imagesToDelete = _context.Images.Where(i => i.ProductId == productId).ToList();

			_context.Images.RemoveRange(imagesToDelete);
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

        public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<Image> GetImageBylink(string link)
		{
			return await _context.Images.FirstOrDefaultAsync(x => x.ImageLink == link);
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
