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
    public class SaleDetailsService : ISaleDetailsService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        public SaleDetailsService(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<SaleDetailsViewModel>> GetAllSale(Expression<Func<SaleDetails, bool>> filter, Func<IQueryable<SaleDetails>, IOrderedQueryable<SaleDetails>> orderby = null, params Expression<Func<SaleDetails, object>>[] includes)
        {
            var list = await _uow.GetRepository<SaleDetails>().GetAll(filter, orderby, includes);
            return _mapper.Map<List<SaleDetailsViewModel>>(list);
        }
    }
}
