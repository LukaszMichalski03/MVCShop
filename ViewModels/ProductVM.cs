using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.ViewModels
{
    public class ProductVM
    {
       
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int? StockQuantity { get; set; }
		public IEnumerable<IFormFile?> Images { get; set; }
		public IEnumerable<string?> ImagesLinks { get; set; }

	}
}
