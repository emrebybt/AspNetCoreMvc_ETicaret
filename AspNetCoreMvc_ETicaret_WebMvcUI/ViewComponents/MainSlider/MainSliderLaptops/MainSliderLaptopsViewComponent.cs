using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.MainSlider.MainSliderLaptops
{
    public class MainSliderLaptopsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainSliderLaptopsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == 6, c => c.Category);
            return View(values);
        }
    }
}
