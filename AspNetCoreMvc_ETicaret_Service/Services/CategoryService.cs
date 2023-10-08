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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public CategoryService(IUnitOfWorks uow, IMapper mapper, IProductService productService)
        {
            _uow = uow;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var list = await _uow.GetRepository<Categories>().GetAllAsync();
            var mappedlist = _mapper.Map<List<CategoryViewModel>>(list);
            foreach (var item in mappedlist)
            {
                item.ProductCount = await _productService.GetCountByCategory(item.Id);
            }
            return mappedlist;
        }

        public async Task<List<CategoryViewModel>> GetAllByFilter(Expression<Func<Categories, bool>> filter, Func<IQueryable<Categories>, IOrderedQueryable<Categories>> orderby = null, params Expression<Func<Categories, object>>[] includes)
        {
            var list = await _uow.GetRepository<Categories>().GetAll(filter, orderby, includes);
            return _mapper.Map<List<CategoryViewModel>>(list);
        }
    }
}
