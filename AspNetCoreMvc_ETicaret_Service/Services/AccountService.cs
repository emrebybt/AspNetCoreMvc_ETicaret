using AspNetCoreMvc_ETicaret_DataAccess.Identity;
using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly ICartLineService _lineService;
        private readonly IUnitOfWorks _uow;

        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper, ICartService cartService, ICartLineService lineService, IUnitOfWorks uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _cartService = cartService;
            _lineService = lineService;
            _uow = uow;
        }

        public async Task<string> CreateUserAsync(RegisterViewModel model)
        {
            string message = string.Empty;
            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };

            var identityResult = await _userManager.CreateAsync(user, model.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                message = "OK";

            }
            foreach (var error in identityResult.Errors)
            {
                message = error.Description;
            }
            return message;
        }

        public async Task<string> FinByNameAsync(LoginViewModel model, List<CartLineViewModel> cartline)
        {
            string message = string.Empty;
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                message = "user not found";
                return message;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (signInResult.Succeeded)
            {
                var carts = _cartService.GetUserCart(user.Id);
                if (cartline.Count() > 0 && carts != null)
                {
                    var dbcart = _lineService.GetCartLines(carts.Id);
                    _lineService.MoveCartLineSessionToDb(cartline, carts.Id ,dbcart);
                    _cartService.UpdateCartPrice(carts.Id);
                    
                }
                if (cartline.Count() > 0 && carts == null)
                {
                    int newcart = _cartService.CreateCart(user.Id, _lineService.TotalPrince(cartline));
                    List<CartLineViewModel> cartLinemodel = new List<CartLineViewModel>();
                    _lineService.MoveCartLineSessionToDb(cartline, newcart, cartLinemodel);
                    _cartService.UpdateCartPrice(newcart);
                }
                message = "OK";

            }
            return message;
        }

        public async Task<UserViewModel> Find(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> FindByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var list = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserViewModel>>(list);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public void UpdateCartPrice(int cartId)
        {
            var cart = _cartService.GetCart(cartId);
            cart.TotalPrice = _lineService.CartTotalPrice(cartId);
            _uow.GetRepository<Cart>().Update(_mapper.Map<Cart>(cart));
            _uow.Commit();
        }
    }
}
