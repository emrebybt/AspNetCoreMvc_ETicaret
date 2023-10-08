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
    public interface IFilterSpecService
    {
        Task<List<FilterSpecsViewModel>> GetAll(Expression<Func<FilterSpecs, bool>> filter = null, Func<IQueryable<FilterSpecs>, IOrderedQueryable<FilterSpecs>> orderby = null, params Expression<Func<FilterSpecs, object>>[] includes);
    }
}
