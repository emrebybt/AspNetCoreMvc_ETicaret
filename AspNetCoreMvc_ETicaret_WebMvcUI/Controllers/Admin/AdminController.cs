using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers.Admin
{

    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IProductSpecsService _productSpecsService;
        public AdminController(IProductService productService, ICategoryService categoryService, IAccountService accountService, IProductSpecsService productSpecsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _accountService = accountService;
            _productSpecsService = productSpecsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Products()
        {
            var products = _productService.GetAllByFilterNoAsync(null, x => x.OrderBy(x => x.Category), x => x.Category);
            return View(products);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetById(id);
            return View(product);
        }
        public async Task<IActionResult> Categories()
        {
            var list = await _categoryService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Users()
        {
            var list = await _accountService.GetAll();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAll();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel product, IFormFile formFile, IFormFile formFile2, IFormFile formFile3, IFormFile formFile4, IFormFile formFile5)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile2.FileName);
            var path3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile3.FileName);
            var path4 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile4.FileName);
            var path5 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile5.FileName);
            var stream = new FileStream(path, FileMode.Create);
            var stream2 = new FileStream(path2, FileMode.Create);
            var stream3 = new FileStream(path3, FileMode.Create);
            var stream4 = new FileStream(path4, FileMode.Create);
            var stream5 = new FileStream(path5, FileMode.Create);
            formFile.CopyTo(stream);
            formFile2.CopyTo(stream2);
            formFile3.CopyTo(stream3);
            formFile4.CopyTo(stream4);
            formFile5.CopyTo(stream5);
            product.ThumbnailImage = "/images/" + formFile.FileName;
            product.ContentImage = "/images/" + formFile2.FileName;
            product.ContentImage2 = "/images/" + formFile3.FileName;
            product.ContentImage3 = "/images/" + formFile4.FileName;
            product.ContentImage4 = "/images/" + formFile5.FileName;
            await _productService.Add(product);
            return RedirectToAction("CreateProductSpecs",new {CategoryId = product.CategoryId,ProductId = product.Id});  //Product id gelmiyor
        }
        [HttpGet]
        public async Task<IActionResult> CreateProductSpecs(int CategoryId, int ProductId)
        {
            var specs = await _productSpecsService.GetListAllByFilter(x => x.Product.CategoryId == CategoryId, x=>x.Product);
            ViewBag.productId = ProductId;
            return View(specs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductSpecs(Dictionary<string, string> formData,int productId)
        {
            ProductSpecsViewModel model = new ProductSpecsViewModel();
            foreach (var item in formData)
            {
                model.Key=item.Key;
                model.Value=item.Value;
                model.ProductId = productId;
                await _productSpecsService.Add(model);
            }
            return RedirectToAction("Products");
        }
    }
}
