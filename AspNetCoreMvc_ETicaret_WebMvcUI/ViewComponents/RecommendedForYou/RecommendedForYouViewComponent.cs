using AspNetCoreMvc_ETicaret_Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.ViewComponents.RecommendedForYou
{
    public class RecommendedForYouViewComponent : ViewComponent
    {
        private readonly IProductService _service;

        public RecommendedForYouViewComponent(IProductService service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            int categoryId = Convert.ToInt32(Request.Cookies["category"]);
            var list = _service.GetAllByFilterNoAsync(x=>x.IsDeleted == false,null,c=>c.Category).TakeLast(4).Reverse().ToList();
            if (categoryId > 0)
            {
               list = _service.GetAllByFilterNoAsync(x => x.CategoryId == categoryId && x.IsDeleted == false, null, c=>c.Category).TakeLast(4).Reverse().ToList();
            }
            return View(list);
        }
    }
}
