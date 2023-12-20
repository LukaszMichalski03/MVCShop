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
        public ICollection<Product> ListedProducts { get; set; }
        public string? ProfilePictureLink { get; set; }
        public ICollection<ShoppingCard> ShoppingCards { get; set; }
    }
}
