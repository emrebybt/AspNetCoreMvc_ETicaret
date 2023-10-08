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
    public interface ISaleService
    {
        void CreateSale(List<CartLineViewModel> cartline, CartViewModel cart);
        Task<List<SaleViewModel>> GetAllSale(Expression<Func<Sale, bool>> filter, Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderby = null, params Expression<Func<Sale, object>>[] includes);
    }
}
