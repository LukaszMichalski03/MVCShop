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
		private readonly IPhotoService _photoService;

		public ProductsController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IProductRepository productRepository
            , IPhotoService photoService)
        {
            _logger = logger;
            this._userManager = userManager;
            _productRepository = productRepository;
			this._photoService = photoService;
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
            var images = await _productRepository.GetProductsImagesAsync(id);
            ProductVM productVM = new ProductVM()
            {
                
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImagesLinks = images
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
			foreach (var file in addProductVM.Images)
			{
				if (file != null && file.Length > 0)
				{
					string imageUrl = await _photoService.AddPhotoAsync(file);

					if (!string.IsNullOrEmpty(imageUrl))
					{
						_productRepository.AddImage(
							new Image()
							{
								ImageLink = imageUrl,
								ProductId = product.Id
							});

						await _userManager.UpdateAsync(user);
					}
				}
			}



			var result = _productRepository.Update(product);
            if(result == true) return RedirectToAction("Index", "Products");
            else
            {
                TempData["ErrorMessage"] = "Nie udało się edytować.";
                return RedirectToAction("Index", "Products");
            }
        }
		[HttpPost]
		public async Task<IActionResult> DeleteImage(string imagelink)
		{
			Image image = await _productRepository.GetImageBylink(imagelink);
			if (image == null)
			{
				return RedirectToAction("Index", "Products");
			}
            var result = await _photoService.DeletePhotoAsync(imagelink);
			_productRepository.DeleteImage(image);

			return RedirectToAction("Index", "Products");
		}
		[HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            Product product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            var result = _productRepository.DeleteProductsImages(productId);
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

			foreach (var file in addProductVM.Images)
			{
				if (file != null && file.Length > 0)
				{
					string imageUrl = await _photoService.AddPhotoAsync(file);

					if (!string.IsNullOrEmpty(imageUrl))
					{
                        _productRepository.AddImage(
                            new Image() { 
                                ImageLink = imageUrl ,
                                ProductId = product.Id
                            });
                        
						await _userManager.UpdateAsync(user);
					}
				}
			}

			
            return View(addProductVM);
        }
    }
}
