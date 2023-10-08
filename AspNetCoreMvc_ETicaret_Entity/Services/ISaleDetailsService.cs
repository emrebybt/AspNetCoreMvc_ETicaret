using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Services
{
    public interface ISaleDetailsService
    {
        Task<List<SaleDetailsViewModel>> GetAllSale(Expression<Func<SaleDetails, bool>> filter, Func<IQueryable<SaleDetails>, IOrderedQueryable<SaleDetails>> orderby = null, params Expression<Func<SaleDetails, object>>[] includes);
    }
}
