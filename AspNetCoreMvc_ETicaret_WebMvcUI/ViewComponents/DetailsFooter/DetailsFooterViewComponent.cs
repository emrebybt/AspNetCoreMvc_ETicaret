using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.DetailsFooter
{
    public class DetailsFooterViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public DetailsFooterViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var list = await _productService.GetListAllByFilter(x => x.CategoryId == id && x.IsDeleted == false,x=>x.Category);
            return View(list.ToList().TakeLast(5));
        }
    }
}
