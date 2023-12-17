using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Migrations;
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
        [HttpGet] 
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            ProductVM productVM = new ProductVM()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,ProductVM addProductVM)
        {
            var user = await _userManager.GetUserAsync(User);
            Product product = new Product()
            {
                Name = addProductVM.Name,
                Description = addProductVM.Description,
                Price = addProductVM.Price,
                StockQuantity = addProductVM.StockQuantity,
                Id = id,
                AppUserId = user.Id
            };
            var result = _productRepository.Update(product);
            if(result == true) return RedirectToAction("Index", "Products");
            else
            {
                TempData["ErrorMessage"] = "Nie udało się edytować.";
                return RedirectToAction("Index", "Products");
            }
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
        public async Task<IActionResult> Add(ProductVM addProductVM)
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
