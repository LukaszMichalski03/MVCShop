using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegisterIdentity.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPhotoService _photoService;

        public OrdersController(IProductRepository productRepository,IOrderRepository orderRepository, IProfileRepository profileRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPhotoService photoService)
        {
            this._productRepository = productRepository;
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
        [HttpPost]
        public async Task<IActionResult> Add(List<ShoppingCartVM> shoppingCartVMs)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            Order order = new Order()
            {
                UserId = user.Id,
                Date = DateTime.Now,

            };
            
            bool orderresult = _orderRepository.Add(order);
            if (orderresult)
            {
                foreach(var VM in shoppingCartVMs)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductId = VM.Product.Id
                    };
                    _orderRepository.AddOrderProduct(orderProduct);
                    ShoppingCard shoppingCart = await _productRepository.GetShoppingCartItemByIdAsync(VM.ShoppingCartId);
                    
                    _productRepository.DeleteShoppinngCart(shoppingCart);
                }
            }
            
            return RedirectToAction("ShoppingCart", "Home");
        }
    }
}
