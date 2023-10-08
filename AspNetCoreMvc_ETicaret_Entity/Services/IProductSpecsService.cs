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
    public interface IProductSpecsService
    {
        Task<IEnumerable<ProductSpecsViewModel>> GetListAllByFilter(Expression<Func<ProductSpecs, bool>> filter, params Expression<Func<ProductSpecs, object>>[] includes);
        Task Add(ProductSpecsViewModel model);
    }
}
