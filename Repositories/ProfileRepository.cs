using LoginRegisterIdentity.Data;
using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterIdentity.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
			return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
		}
		public async Task<AppUser> GetUserByIdAsyncNoTracking(string id)
		{
			return await _context.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0;
		}

		public bool Update(AppUser user)
		{
			_context.Update(user);
			return Save();
		}
	}
}
