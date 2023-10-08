using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.Orders
{
    public class OrdersViewComponent : ViewComponent
    {
        private readonly ISaleService _saleService;
        private readonly ISaleDetailsService _saleDetailsService;
        private readonly IAccountService _accountService;

        public OrdersViewComponent(ISaleService saleService, IAccountService accountService, ISaleDetailsService saleDetailsService)
        {
            _saleService = saleService;
            _accountService = accountService;
            _saleDetailsService = saleDetailsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _accountService.Find(User.Identity.Name);
            var list = await _saleService.GetAllSale(x=>x.UserId == user.Id && x.IsDeleted == false,x=>x.OrderByDescending(x=>x.CreatedDate),x=>x.SaleDetails);
            return View(list);
        }
    }
}
