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
    public class FilterSpecService : IFilterSpecService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;

        public FilterSpecService(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<List<FilterSpecsViewModel>> GetAll(Expression<Func<FilterSpecs, bool>> filter = null, Func<IQueryable<FilterSpecs>, IOrderedQueryable<FilterSpecs>> orderby = null, params Expression<Func<FilterSpecs, object>>[] includes)
        {
            var list = await _uow.GetRepository<FilterSpecs>().GetAll(filter,orderby,includes);
            return _mapper.Map<List<FilterSpecsViewModel>>(list);
        }
    }
}
