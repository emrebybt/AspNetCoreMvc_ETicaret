using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFilterSpecService _filterSpecService;
        private readonly IProductSpecsService _productSpecsService;
        CookieOptions options = new CookieOptions();
        public UrunlerController(IProductService productService, IFilterSpecService filterSpecService, IProductSpecsService productSpecsService)
        {
            _productService = productService;
            _filterSpecService = filterSpecService;
            _productSpecsService = productSpecsService;
        }
        
        public async Task<IActionResult> Index(int? id, string[]? value , string search, string brand)
        {
            var products = await _productService.GetListAllByFilter(x => x.IsDeleted == false, x => x.Category);
            if (id != null)
            {
                products = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == id, x => x.Category);
            }
            options.Expires = DateTime.Now.AddDays(1);
            if (value.Count() > 0)
            {
                var values = await _productService.GetProductsBySpecs(products.ToList(), id, value);
                ViewBag.specs = await _filterSpecService.GetAll(x => x.CategoryId == id);
                return View(values);
            }
            if (search != null)
            {
                products = products.Where(x => x.Name.ToLower().Contains(search.ToLower()));
            }
            if (brand != null)
            {
                products = products.Where(x => x.Brand.Contains(brand));
            }
            ViewBag.specs = await _filterSpecService.GetAll(x => x.CategoryId == id);
            Response.Cookies.Append("category", id.ToString(), options);
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {

            var model = await _productService.GetByFilter(x => x.Id == id,x=>x.Category);
            ViewBag.specs = await _productSpecsService.GetListAllByFilter(x => x.ProductId == id);
            return View(model);
        }
    }
}
