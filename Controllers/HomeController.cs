using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.ViewModels;
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
            List<ShoppingCartVM> shoppingCartVMs = new List<ShoppingCartVM>();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var cardItems = await _productRepository.GetShoppingCardItemsByUserId(user.Id);
            foreach (var item in cardItems)
            {
                shoppingCartVMs.Add(new ShoppingCartVM
                {
                    ShoppingCartId = item.Id,
                    Product = await _productRepository.GetProductByIdAsync(item.ProductId),
                });
            }
            return View(shoppingCartVMs);
        }
        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            
            ShoppingCard item = new ShoppingCard()
            {
                AppUserId = user.Id,
                ProductId = productId,
                
            };
            _productRepository.AddToCard(item);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int itemId)
        {
            
            var item = await _productRepository.GetShoppingCartItemByIdAsync(itemId);
            _productRepository.DeleteFromCard(item);
           

            
            return RedirectToAction("ShoppingCart", "Home");
        }
    }
}
