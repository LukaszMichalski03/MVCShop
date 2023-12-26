using LoginRegisterIdentity.Models;

namespace LoginRegisterIdentity.Interfaces
{
	public interface IProductRepository
	{
		Task<Product> GetProductByIdAsync(int? id);
		Task<ShoppingCard> GetShoppingCartItemByIdAsync(int id);
		Task<IEnumerable<Product>> GetAllProductsAsync();
		Task<IEnumerable<Product>> GetUsersProductsAsync(string AppUserId);
		Task<IEnumerable<ShoppingCard>> GetShoppingCardItemsByUserId(string userId);
        Task<IEnumerable<string>> GetProductsImagesAsync(int productId);
        Task<Image> GetImageBylink(string link);

        bool Save();
		bool Add(Product product);
		bool AddImage(Image image);
		bool AddToCard(ShoppingCard item);
		

        
		bool Update(Product product);
		bool Delete(Product product);
		bool DeleteProductsImages(int productId);
		bool DeleteShoppinngCart(ShoppingCard shoppinngcart);
		bool DeleteImage(Image image);
        bool DeleteFromCard(ShoppingCard item);
    }
}
