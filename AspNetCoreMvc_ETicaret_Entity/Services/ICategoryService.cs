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
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<List<CategoryViewModel>> GetAllByFilter(Expression<Func<Categories, bool>> filter, Func<IQueryable<Categories>, IOrderedQueryable<Categories>> orderby = null, params Expression<Func<Categories, object>>[] includes);
    }
}
