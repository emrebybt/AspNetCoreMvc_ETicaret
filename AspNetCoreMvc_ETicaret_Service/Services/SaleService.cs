using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Service.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly ICartLineService _lineService;
        private readonly IProductService _productService;

        public SaleService(IUnitOfWorks uow, IMapper mapper, ICartService cartService, ICartLineService lineService, IProductService productService)
        {
            _uow = uow;
            _mapper = mapper;
            _cartService = cartService;
            _lineService = lineService;
            _productService = productService;
        }

        public void CreateSale(List<CartLineViewModel> cartline, CartViewModel cart)
        {
            Sale sale = new Sale();
            sale.UserId = cart.UserId;
            sale.TotalPrice = cart.TotalPrice;
            sale.CreatedDate = DateTime.Now;
            _uow.GetRepository<Sale>().AddNotAsync(sale);
            _uow.Commit();
            _cartService.DeleteCart(cart);
            _lineService.Delete(cart.Id);
            foreach (var item in cartline)
            {
                SaleDetailsViewModel saleDetail = new SaleDetailsViewModel();
                saleDetail.Price = item.Price;
                saleDetail.Quantity = item.Quantity;
                saleDetail.SaleId = sale.Id;
                saleDetail.ProductId = item.ProductId;
                _uow.GetRepository<SaleDetails>().AddNotAsync(_mapper.Map<SaleDetails>(saleDetail));
                var product = _productService.GetByIdNotAsync(item.ProductId);
                if (product.Stock > 1)
                {
                    product.Stock -= item.Quantity;
                    _productService.Update(product);
                }
                else
                {
                    _productService.Delete(product);
                }
                
                _uow.Commit();
            }

        }

        public async Task<List<SaleViewModel>> GetAllSale(Expression<Func<Sale, bool>> filter, Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderby = null, params Expression<Func<Sale, object>>[] includes)
        {
            var list = await _uow.GetRepository<Sale>().GetAll(filter,orderby,includes);
            return _mapper.Map<List<SaleViewModel>>(list);
        }
    }
}
