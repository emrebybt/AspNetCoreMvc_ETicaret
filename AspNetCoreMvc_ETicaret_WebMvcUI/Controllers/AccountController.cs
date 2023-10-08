using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_Service.Services;
using AspNetCoreMvc_ETicaret_WebMvcUI.SessionExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly ICartLineService _lineService;
        private readonly ICartService _cartService;
        public AccountController(IAccountService service, ICartLineService lineService, ICartService cartService)
        {
            _service = service;
            _lineService = lineService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string msg = await _service.CreateUserAsync(model);
            if (msg == "OK")
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", msg);
            }
            return View(model);
        }

        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            var msg = await _service.FinByNameAsync(model,cart);

            if (msg == "user not found")
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }
            else if (msg == "OK")
            {
                HttpContext.Session.Remove("cart");
                return Redirect(model.ReturnUrl ?? "~/");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Hatalı");
            }
            //login olduğunda sessionda sepet varmı diye kontrol edilecek varsa giriş yapan kullanıcıya ait karta kayıt edilecek
            

            
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction("Index", "Anasayfa");
        }

        public async Task<IActionResult> UserDashboard()
        {
            ViewBag.user = await _service.Find(User.Identity.Name);
            
            return View();
        }

    }
}
