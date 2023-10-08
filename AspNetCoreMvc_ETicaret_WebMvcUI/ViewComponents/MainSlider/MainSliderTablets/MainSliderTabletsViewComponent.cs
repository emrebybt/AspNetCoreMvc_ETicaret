using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.MainSlider.MainSliderTablets
{
    public class MainSliderTabletsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainSliderTabletsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == 2, c => c.Category);
            return View(values);
        }
    }
}
