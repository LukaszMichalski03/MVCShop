using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegisterIdentity.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;

        public ProductsController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IProductRepository productRepository)
        {
            _logger = logger;
            this._userManager = userManager;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            
            string AppUserId = user.Id;
            var products = await _productRepository.GetUsersProductsAsync(AppUserId);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Product product =await _productRepository.GetProductByIdAsync(id);
            if(product == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            Product product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            _productRepository.Delete(product);

            return RedirectToAction("Index", "Products");
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductVM addProductVM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            

            Product product = new Product()
            {
                Name = addProductVM.Name,
                Description = addProductVM.Description,
                Price = addProductVM.Price,
                StockQuantity = addProductVM.StockQuantity,
                AppUserId = user.Id
            };
            _productRepository.Add(product);
            return View(addProductVM);
        }
    }
}
