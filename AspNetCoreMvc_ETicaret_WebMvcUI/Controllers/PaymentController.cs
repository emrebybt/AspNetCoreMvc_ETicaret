using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        private readonly ICartLineService _lineService;
        private readonly ISaleService _saleService;

        public PaymentController(IAccountService accountService, ICartService cartService, ICartLineService lineService, ISaleService saleService)
        {
            _accountService = accountService;
            _cartService = cartService;
            _lineService = lineService;
            _saleService = saleService;
        }
        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _accountService.FindByIdAsync(userId);
            ViewBag.user = user;
            var carts = _cartService.GetUserCart(userId);
            var cartlines = _lineService.GetCartLines(carts.Id);
            ViewBag.cart = carts;
            return View(cartlines);
        }
        public IActionResult Payment()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = _cartService.GetUserCart(userId);
            var cartlines = _lineService.GetCartLines(cart.Id);
            _saleService.CreateSale(cartlines, cart);
            return RedirectToAction("UserDashboard","Account");
        }
    }
}
