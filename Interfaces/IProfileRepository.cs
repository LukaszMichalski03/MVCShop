using LoginRegisterIdentity.Models;
using Microsoft.Identity.Client;

namespace LoginRegisterIdentity.Interfaces
{
    public interface IProfileRepository
    {
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByIdAsyncNoTracking(string id);
        public bool Save();
        public bool Update(AppUser user);
    }
}
