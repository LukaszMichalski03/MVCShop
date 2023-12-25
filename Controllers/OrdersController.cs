using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegisterIdentity.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPhotoService _photoService;

        public OrdersController(IOrderRepository orderRepository, IProfileRepository profileRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPhotoService photoService)
        {
            this._orderRepository = orderRepository;
            this._profileRepository = profileRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._photoService = photoService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByUser(user.Id);
            return View(orders);
        }
    }
}
