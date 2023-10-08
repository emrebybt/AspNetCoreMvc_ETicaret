using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Service.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private readonly ICartLineService _lineService;

        public CartService(IUnitOfWorks uow, IMapper mapper, ICartLineService lineService)
        {
            _uow = uow;
            _mapper = mapper;
            _lineService = lineService;
        }

        public int CreateCart(int id, decimal totalprice)
        {
            Cart cart = new Cart()
            {
                UserId = id,
                TotalPrice = totalprice
            };
            _uow.GetRepository<Cart>().Add(cart);
            _uow.Commit();
            return cart.Id;
        }

        public void DeleteCart(CartViewModel cart)
        {
            Cart cart2 = new Cart();
            cart2.TotalPrice = cart.TotalPrice;
            cart2.CreatedDate = DateTime.Now;
            cart2.UserId = cart.UserId;
            cart2.Id = cart.Id;
            cart2.IsDeleted = true;
            _uow.GetRepository<Cart>().Update(cart2);
            _uow.Commit();
        }

        public CartViewModel GetCart(int id)
        {
            var cart = _uow.GetRepository<Cart>().GetNotAsync(x => x.Id == id && x.IsDeleted == false);
            return _mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel GetUserCart(int id)
        {
            var cart = _uow.GetRepository<Cart>().GetNotAsync(x => x.UserId == id && x.IsDeleted == false);
            return _mapper.Map<CartViewModel>(cart);
        }

        public void UpdateCartPrice(int cartId)
        {
            var cart = this.GetCart(cartId);
            Cart newcart = new Cart();
            newcart.UserId = cart.UserId;
            newcart.TotalPrice = _lineService.CartTotalPrice(cartId);
            newcart.IsDeleted = false;
            newcart.CreatedDate = DateTime.Now;
            newcart.Id = cartId;
            _uow.GetRepository<Cart>().Update(newcart);
            _uow.Commit();
        }
    }
}
