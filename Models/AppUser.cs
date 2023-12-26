using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Product>? ListedProducts { get; set; }
        public string? ProfilePictureLink { get; set; }
        public List<ShoppingCard>? ShoppingCards { get; set; }
    }
}
