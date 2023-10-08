using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_Service.Services;
using AspNetCoreMvc_ETicaret_WebMvcUI.SessionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.CartCount
{
    public class CartCountViewComponent : ViewComponent
    {

        private readonly ICartLineService _cartLineService;
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartLineService cartLineService, IAccountService accountService, ICartService cartService)
        {
            _cartLineService = cartLineService;
            _accountService = accountService;
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartline = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _accountService.Find(User.Identity.Name);
                var cart = _cartService.GetUserCart(user.Id);
                if (cart != null)
                {
                    cartline = _cartLineService.GetCartLines(cart.Id);
                }
            }
            return View(cartline.Count());
        }
    }
}
