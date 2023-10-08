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
    public class CartLineService : ICartLineService
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _uow;


        public CartLineService(IProductService productService, IMapper mapper, IUnitOfWorks uow)
        {
            _productService = productService;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<List<CartLineViewModel>> AddCart(List<CartLineViewModel> cartline, int id, int quantity)
        {
            var product = await _productService.GetById(id);
            CartLineViewModel newcartLine = new CartLineViewModel();
            newcartLine.Quantity = quantity;
            newcartLine.Product = _mapper.Map<Products>(product);
            newcartLine.ProductId = product.Id;
            newcartLine.TotalPrince = TotalPrince(cartline);
            newcartLine.Price = product.Price;

            if (cartline.Any(cl => cl.ProductId == newcartLine.ProductId))
            {
                foreach (CartLineViewModel item in cartline)
                {
                    if (item.ProductId == newcartLine.ProductId && item.Quantity < product.Stock)
                    {
                        item.Quantity += newcartLine.Quantity;
                    }
                }
            }
            else
            {
                cartline.Add(newcartLine);
            }
            return cartline;

        }

        public decimal CartTotalPrice(int id)
        {
            IEnumerable<CartLine> list = _uow.GetRepository<CartLine>().GetAllNotAsync(x => x.CartId == id && x.IsDeleted == false);
            var total = list.GroupBy(x => x.CartId).Select(group => new { CartId = group.Key, TotalPrince = group.Sum(p => p.TotalPrince) }).ToList();
            if (total.Count() > 0)
            {
                return total[0].TotalPrince;
            }
            return 0;
        }

        public void CreateCartLine(List<CartLineViewModel> model, int cartid, int quantity, int productId)
        {
            var cartlines = _uow.GetRepository<CartLine>().GetAllNotAsync(x => x.CartId == cartid && x.IsDeleted == false);
            var product = _productService.GetByIdNotAsync(productId);
            foreach (var item in model)
            {
                if (cartlines.Any(x => x.ProductId == item.ProductId))
                {
                    foreach (var cart in cartlines)
                    {
                        if (item.ProductId == cart.ProductId && cart.ProductId == productId && item.Quantity < product.Stock)
                        {
                            cart.Quantity += quantity;
                            cart.TotalPrince = item.Quantity * item.Price;
                            cart.Price = item.Price;
                            _uow.GetRepository<CartLine>().Update(cart);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    CartLine cartline = new CartLine()
                    {
                        ProductId = item.ProductId,
                        Quantity = quantity,
                        Price = item.Price,
                        CartId = cartid,
                        TotalPrince = item.Quantity * item.Price
                    };
                    _uow.GetRepository<CartLine>().AddNotAsync(cartline);
                }
            }

            _uow.Commit();
        }

        public void DecreaseCartLine(List<CartLineViewModel> model, int productId)
        {
            foreach (var item in model)
            {
                if (item.ProductId == productId)
                {
                    item.Quantity--;
                }
            }
        }
        public void DecreaseUserCartLine(List<CartLineViewModel> model, int productId)
        {
            foreach (var item in model)
            {
                if (item.ProductId == productId)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity--;
                        _uow.GetRepository<CartLine>().Update(_mapper.Map<CartLine>(item));
                        _uow.Commit();
                    }
                    else
                    {
                        this.DeleteCartLine(productId);
                    }

                }
            }
        }
        public void Delete(int cartId)
        {
            List<CartLine> deletedCartLine = new List<CartLine>();
            deletedCartLine = _uow.GetRepository<CartLine>().GetAllNotAsync(x => x.CartId == cartId, null).ToList();
            foreach (var cart in deletedCartLine)
            {
                CartLineViewModel cartline = new CartLineViewModel();
                cartline.ProductId = cart.ProductId;
                cartline.Quantity = cart.Quantity;
                cartline.TotalPrince = cart.TotalPrince;
                cartline.Price = cart.Price;
                cartline.CartId = cart.CartId;
                cartline.Id = cart.Id;
                cartline.IsDeleted = true;
                _uow.GetRepository<CartLine>().Update(_mapper.Map<CartLine>(cartline));
                _uow.Commit();
            }
        }

        public void DeleteCartLine(int id)
        {
            var cartline = _uow.GetRepository<CartLine>().GetNotAsync(x => x.ProductId == id);
            _uow.GetRepository<CartLine>().FullDelete(cartline.Id);
            _uow.Commit();
        }

        public CartLineViewModel Get(int id)
        {
            var cartline = _uow.GetRepository<CartLine>().GetNotAsync(x => x.Id == id);
            return _mapper.Map<CartLineViewModel>(cartline);
        }

        public List<CartLineViewModel> GetCartLines(int id)
        {
            var cartlines = _uow.GetRepository<CartLine>().GetAllNotAsync(x => x.CartId == id && x.IsDeleted == false, null, x => x.Product);
            return _mapper.Map<List<CartLineViewModel>>(cartlines);
        }

        public void MoveCartLineSessionToDb(List<CartLineViewModel> model, int id, List<CartLineViewModel> dbcart)
        {
            foreach (var item in model)
            {
                if (dbcart.Any(x => x.ProductId == item.ProductId))
                {
                    foreach (var dbcartline in dbcart)
                    {
                        if (item.ProductId == dbcartline.ProductId)
                        {
                            dbcartline.Quantity += item.Quantity;
                        }
                        dbcartline.Cart = null;
                        dbcartline.Product = null;
                        _uow.GetRepository<CartLine>().Update(_mapper.Map<CartLine>(dbcartline));

                    }
                    _uow.Commit();
                }
                else
                {
                    CartLine cartLine = new()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        TotalPrince = this.TotalPrince(model),
                        CartId = id
                    };
                    _uow.GetRepository<CartLine>().AddNotAsync(cartLine);
                }
            }
            _uow.Commit();
        }

        public List<CartLineViewModel> RemoveCart(List<CartLineViewModel> cartline, int id)
        {
            cartline.RemoveAll(cl => cl.ProductId == id);
            return cartline;
        }
        public decimal TotalPrince(List<CartLineViewModel> cartline)
        {
            decimal total = 0;
            if (cartline.Count != null)
            {
                foreach (var item in cartline)
                {
                    total += item.Quantity * item.Price;
                }
            }
            return total;
        }

    }
}
