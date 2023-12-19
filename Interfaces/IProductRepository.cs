using LoginRegisterIdentity.Models;

namespace LoginRegisterIdentity.Interfaces
{
	public interface IProductRepository
	{
		Task<Product> GetProductByIdAsync(int id);
		Task<IEnumerable<Product>> GetAllProductsAsync();
		Task<IEnumerable<Product>> GetUsersProductsAsync(string AppUserId);
		bool Save();
		bool Add(Product product);
		bool AddImage(Image image);
		Task<IEnumerable<string>> GetProductsImagesAsync(int productId);
		Task<Image> GetImageBylink(string link);
		bool Update(Product product);
		bool Delete(Product product);
		bool DeleteImage(Image image);
		
	}
}
