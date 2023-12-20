using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginRegisterIdentity.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager , IProductRepository productRepository)
        {
           _logger = logger;
            this._userManager = userManager;
            _productRepository = productRepository;
        }
        //public async IActionResult Index()
        //{
            
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }
        //[Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var cardItems = await _productRepository.GetShoppingCardItemsByUserId(user.Id);
            return View(cardItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            
            ShoppingCard item = new ShoppingCard()
            {
                AppUserId = user.Id,
                ProductId = productId,
            };
            _productRepository.AddToCard(item);
            return RedirectToAction("Index", "Home");
        }
    }
}
