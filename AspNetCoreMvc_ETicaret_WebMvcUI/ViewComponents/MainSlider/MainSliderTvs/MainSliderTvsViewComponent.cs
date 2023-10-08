using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.MainSlider.MainSliderTvs
{
    public class MainSliderTvsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainSliderTvsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == 3, c => c.Category);
            return View(values);
        }
    }
}
