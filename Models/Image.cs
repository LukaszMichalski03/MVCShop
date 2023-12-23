using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.Models
{
	public class Image
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        [Required]
        public string? ImageLink { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
