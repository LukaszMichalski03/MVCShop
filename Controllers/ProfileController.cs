using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegisterIdentity.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
		private readonly UserManager<AppUser> _userManager;

		public ProfileController(IProfileRepository profileRepository, UserManager<AppUser> userManager)
        {
            this._profileRepository = profileRepository;
			this._userManager = userManager;
		}
       
		[HttpGet]
        [Authorize]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return RedirectToAction("Index", "Home");

			}

			ProfileVM profile = new ProfileVM()
			{
				Name = user.Name,
				Email = user.Email,
				Address = user.Address,
			};

			return View(profile);
		}
		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Index( ProfileVM profileVM )
        {
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("Index", "Home");
			}
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }

            user.Name = profileVM.Name;
			user.Address = profileVM.Address;
			user.Email = profileVM.Email;

            _profileRepository.Update(user);
			//return RedirectToAction("Index", "Home");
			return View(profileVM);
        }
    }
}
