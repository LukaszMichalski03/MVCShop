using System.ComponentModel.DataAnnotations;

namespace LoginRegisterIdentity.ViewModels
{
    public class ProfileVM
    {
        
        public string? Name { get; set; } = null;
        
        public string? Email { get; set; }

        public IFormFile? Image { get; set; }
        public string? ProfilePictureLink { get; set; }




		public string? Address { get; set; }
    }
}
