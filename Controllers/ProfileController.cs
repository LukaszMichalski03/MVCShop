using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LoginRegisterIdentity.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IPhotoService _photoService;

		public ProfileController(IProfileRepository profileRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPhotoService photoService)
        {
            this._profileRepository = profileRepository;
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._photoService = photoService;
		}
		/// test
		//[HttpGet]
		//public async Task<IActionResult> UserProfile()
		//{
		//	var user = await _userManager.GetUserAsync(User);
		//	return View(user);
		//}

		//[HttpPost]
		//public async Task<IActionResult> UploadProfilePicture(IFormFile file)
		//{
		//	var user = await _userManager.GetUserAsync(User);

		//	if (file != null && file.Length > 0)
		//	{
		//		var imageUrl = await _photoService.AddPhotoAsync(file);

		//		if (!string.IsNullOrEmpty(imageUrl))
		//		{
		//			// Usunięcie starego zdjęcia, jeśli istnieje
		//			if (!string.IsNullOrEmpty(user.ProfilePictureLink))
		//			{
		//				await _photoService.DeletePhotoAsync(user.ProfilePictureLink);
		//			}

		//			// Zapis nowego linku do zdjęcia w modelu użytkownika
		//			user.ProfilePictureLink = imageUrl;
		//			await _userManager.UpdateAsync(user);
		//		}
		//	}

		//	return RedirectToAction("UserProfile");
		//}
		/// test /// test /// test/// test end
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
				ProfilePictureLink = user.ProfilePictureLink,
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
			if (profileVM.Image!= null && profileVM.Image.Length > 0)
			{
				string imageUrl = await _photoService.AddPhotoAsync(profileVM.Image);

				if (!string.IsNullOrEmpty(imageUrl))
				{
					// Usunięcie starego zdjęcia, jeśli istnieje
					if (!string.IsNullOrEmpty(user.ProfilePictureLink))
					{
						await _photoService.DeletePhotoAsync(user.ProfilePictureLink);
					}

					// Zapis nowego linku do zdjęcia w modelu użytkownika
					user.ProfilePictureLink = imageUrl;
					//await _userManager.UpdateAsync(user);
				}
			}
			user.Name = profileVM.Name;
			user.Address = profileVM.Address;
			user.Email = profileVM.Email;
			

            _profileRepository.Update(user);
			//return RedirectToAction("Index", "Home");
			return View(profileVM);
        }
		[HttpPost, Authorize]
		public async Task<IActionResult> Delete()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return RedirectToAction("Index", "Home");

			}
			await _userManager.DeleteAsync(user);
			await _signInManager.SignOutAsync();
			
			return RedirectToAction("Index", "Home");
		}
    }
}
