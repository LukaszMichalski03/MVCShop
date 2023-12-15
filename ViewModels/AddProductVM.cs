using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.ViewModels
{
    public class AddProductVM
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public int StockQuantity { get; set; }

    }
}
