using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_WebMvcUI.SessionExtensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartLineService _cartLineService;
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartLineService cartLineService, IAccountService accountService, ICartService cartService)
        {
            _productService = productService;
            _cartLineService = cartLineService;
            _accountService = accountService;
            _cartService = cartService;
        }
        List<CartLineViewModel> Cart;


        public IActionResult Index()
        {
            Cart = GetCart();
            ViewBag.total = _cartLineService.TotalPrince(Cart);
            return View(Cart);
        }
        public async Task AddCart(int id)
        {
            Cart = GetCart();
            Cart = await _cartLineService.AddCart(Cart, id, 1);
            SaveCart(Cart, 1, id);
        }
        public List<CartLineViewModel> GetCart()
        {
            var cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var carts = _cartService.GetUserCart(userId);
                if (carts != null)
                {
                    cart = _cartLineService.GetCartLines(carts.Id);
                }
            }
            return cart;
        }
        public IActionResult GetCartAjax()
        {
            var cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var carts = _cartService.GetUserCart(userId);
                if (carts != null)
                {
                    cart = _cartLineService.GetCartLines(carts.Id);
                }
            }
            ViewBag.total = _cartLineService.TotalPrince(cart);
            return PartialView("CartPartial", cart);
        }
        public IActionResult DeleteCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                
                _cartLineService.DeleteCartLine(id);
                Cart = GetCart();
            }
            else
            {
                Cart = GetCart();
                Cart = _cartLineService.RemoveCart(Cart, id);
            }
            SaveCart(Cart, 0, 0);
            return RedirectToAction("Index");
        }
        public void SaveCart(List<CartLineViewModel> cartline, int quantity, int productId)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var carts = _cartService.GetUserCart(userId);
            if (User.Identity.IsAuthenticated)
            {
                //Giriş yapmış olan kullanıcının sepeti yok ise yeni bir sepet oluşturuyoruz.
                if (carts == null)
                {
                    //Sepet oluşturma işlemi
                    var cartid = _cartService.CreateCart(userId, _cartLineService.TotalPrince(cartline));
                    //Sepet detayı oluşturma işlemi
                    _cartLineService.CreateCartLine(Cart, cartid, quantity, productId);
                }
                else
                {
                    //Mevcut bir sepeti var ise sadece sepet detayı oluşturuyoruz.
                    _cartLineService.CreateCartLine(Cart, carts.Id, quantity, productId);
                    //Mevcut olan sepetinin toplam tutarını güncelliyoruz.
                    _cartService.UpdateCartPrice(carts.Id);
                }
            }
            else
            {
                HttpContext.Session.SetJson("cart", cartline);
            }
        }
        public IActionResult DropdownRefresh()
        {
            Cart = GetCart();
            ViewBag.total = _cartLineService.TotalPrince(Cart);
            return ViewComponent("CartDropdown", Cart);
        }
        public IActionResult CartCount()
        {
            int count = GetCart().Count();
            return ViewComponent("CartCount", count);
        }
        public void Decrease(int id)
        {
            //var cartline = _cartLineService.Get(id);
            if (User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var cart = _cartService.GetUserCart(userId);
                if (cart != null)
                {
                    var cartlines = _cartLineService.GetCartLines(cart.Id);
                    _cartLineService.DecreaseUserCartLine(cartlines, id);
                }
            }
            else
            {
                Cart = GetCart();
                foreach (var item in Cart)
                {
                    if (item.Quantity > 0)
                    {
                        _cartLineService.DecreaseCartLine(Cart, id);
                        SaveCart(Cart, 0, 0);
                    }
                    else
                    {
                        this.DeleteCart(id);
                    }
                }
            }
        }
    }
}
