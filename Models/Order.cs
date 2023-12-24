using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AppUser? User { get; set; }
        public string? UserId { get; set; }
        [Required]
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
