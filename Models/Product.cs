using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegisterIdentity.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int? StockQuantity { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
		
		public List<Image>? Images { get; set; }

        public List<ShoppingCard>? ShoppingCards { get; set; }


        public int? OrderId { get; set; }        
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }
}
