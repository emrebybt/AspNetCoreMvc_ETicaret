using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.MainSlider.MainSliderAllProducts
{
    public class MainSliderAllProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainSliderAllProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetListAllByFilter(x => x.IsDeleted == false, x => x.Category);
            return View(values);
        }
    }
}
