using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IProductSpecsService _productSpecsService;
        private readonly ISaleService _saleService;
        private readonly ISaleDetailsService _saleDetailsService;
        private readonly ICommentService _commentService;
        public AdminController(IProductService productService, ICategoryService categoryService, IAccountService accountService, IProductSpecsService productSpecsService, ISaleService saleService, ISaleDetailsService saleDetailsService, ICommentService commentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _accountService = accountService;
            _productSpecsService = productSpecsService;
            _saleService = saleService;
            _saleDetailsService = saleDetailsService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var total = await _saleService.GetAll();
            ViewBag.totalsale = total.Sum(x => x.TotalPrice);
            var users = await _accountService.GetAll();
            ViewBag.users = users.Count();
            var totalproduct = await _productService.GetAll();
            ViewBag.totalproduct = totalproduct.Sum(x => x.Stock);
            ViewBag.lastsales = await _saleService.GetAllLastFive();
            var lastproducts = _productService.GetAllByFilterNoAsync(null, x => x.OrderBy(x => x.Category), x => x.Category);
            ViewBag.lastproducts = lastproducts.TakeLast(5);
            return View();
        }
        public IActionResult Products()
        {
            var products = _productService.GetAllByFilterNoAsync(null, x => x.OrderBy(x => x.Category), x => x.Category);
            return View(products);
        }
        public async Task<IActionResult> Sales()
        {
            var list = await _saleService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> SaleDetails(int id)
        {
            var list = await _saleDetailsService.GetAllSale(x => x.SaleId == id, x => x.OrderBy(x => x.CreatedDate), x => x.Product);
            return View(list);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductDetails(ProductViewModel model, IFormFile formFile, IFormFile formFile2, IFormFile formFile3, IFormFile formFile4, IFormFile formFile5)
        {
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                model.ThumbnailImage = "/images/" + formFile.FileName;
            }
            if (formFile2 != null)
            {
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile2.FileName);
                var stream2 = new FileStream(path2, FileMode.Create);
                formFile2.CopyTo(stream2);
                model.ContentImage = "/images/" + formFile2.FileName;
            }
            if (formFile3 != null)
            {
                var path3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile3.FileName);
                var stream3 = new FileStream(path3, FileMode.Create);
                formFile3.CopyTo(stream3);
                model.ContentImage2 = "/images/" + formFile3.FileName;
            }
            if (formFile4 != null)
            {
                var path4 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile4.FileName);
                var stream4 = new FileStream(path4, FileMode.Create);
                formFile4.CopyTo(stream4);
                model.ContentImage3 = "/images/" + formFile4.FileName;
            }
            if (formFile5 != null)
            {
                var path5 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile5.FileName);
                var stream5 = new FileStream(path5, FileMode.Create);
                formFile5.CopyTo(stream5);
                model.ContentImage4 = "/images/" + formFile5.FileName;
            }



            _productService.Update(model);
            return RedirectToAction("Products");
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
        public async Task<IActionResult> ProductStatus(int id)
        {
            var product = await _productService.GetByFilter(x => x.Id == id);
            if (product.IsDeleted == false)
            {
                product.IsDeleted = true;
            }
            else
            {
                product.IsDeleted = false;
            }
            _productService.Update(product);
            return RedirectToAction("Products");
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
            var productId = await _productService.Add(product);
            return RedirectToAction("CreateProductSpecs", new { CategoryId = product.CategoryId, ProductId = productId });
        }
        [HttpGet]
        public async Task<IActionResult> CreateProductSpecs(int CategoryId, int ProductId)
        {
            var specs = await _productSpecsService.GetListAllByFilter(x => x.Product.CategoryId == CategoryId, x => x.Product);
            ViewBag.productId = ProductId;
            specs = specs.Where(x => x.ProductId == specs.Min(x => x.ProductId)).ToList();
            return View(specs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductSpecs(Dictionary<string, string> formData, int productId)
        {
            ProductSpecsViewModel model = new ProductSpecsViewModel();
            foreach (var item in formData)
            {
                if (item.Value != null)
                {
                    model.Key = item.Key;
                    model.Value = item.Value;
                    model.ProductId = productId;
                    await _productSpecsService.Add(model);
                }
            }
            return RedirectToAction("Products");
        }
        public async Task<IActionResult> Comments()
        {
            var comments = await _commentService.GetAllByFilter(null,null,x=>x.Product);
            return View(comments);
        }
        public async Task<IActionResult> CommentDelete(int id)
        {
            var comment = await _commentService.Get(id);
            if (comment.IsDeleted == true)
            {
                comment.IsDeleted = false;
            }
            else
            {
                comment.IsDeleted= true;
            }
            _commentService.Update(comment);
            return RedirectToAction("Comments");
        }
    }
}
