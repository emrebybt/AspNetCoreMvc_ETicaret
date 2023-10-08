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
    public class ProductSpecsService : IProductSpecsService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        public ProductSpecsService(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task Add(ProductSpecsViewModel model)
        {
            await _uow.GetRepository<ProductSpecs>().Add(_mapper.Map<ProductSpecs>(model));
            await _uow.CommitAsync();
        }

        public async Task<IEnumerable<ProductSpecsViewModel>> GetListAllByFilter(Expression<Func<ProductSpecs, bool>> filter, params Expression<Func<ProductSpecs, object>>[] includes)
        {
            var list = await _uow.GetRepository<ProductSpecs>().GetAll(filter,null,includes);
            return _mapper.Map<List<ProductSpecsViewModel>>(list);
        }
        
    }
}
