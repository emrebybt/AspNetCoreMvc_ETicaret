using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.MainSlider.MainSliderPhones
{
    public class MainSliderPhonesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainSliderPhonesViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == 1, c => c.Category);
            return View(values);
        }
    }
}
