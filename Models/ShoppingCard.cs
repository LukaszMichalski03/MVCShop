using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.Models
{
	public class ShoppingCard
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
		public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
