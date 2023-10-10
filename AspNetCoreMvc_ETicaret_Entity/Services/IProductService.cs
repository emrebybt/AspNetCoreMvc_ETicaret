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
    public interface IProductService
    {
        Task<int> Add(ProductViewModel model);
        public void Update(ProductViewModel model);
        public void Delete(ProductViewModel model);
        Task<ProductViewModel> GetById(int id);
        ProductViewModel GetByIdNotAsync(int id);
        Task<IEnumerable<ProductViewModel>> GetListAllByFilter(Expression<Func<Products, bool>> filter, params Expression<Func<Products, object>>[] includes);
        Task<ProductViewModel> GetByFilter(Expression<Func<Products, bool>> filter, params Expression<Func<Products, object>>[] includes);
        Task<List<ProductViewModel>> GetProductsBySpecs(List<ProductViewModel> model, int? id, string[]? value);
        List<ProductViewModel> GetAllByFilterNoAsync(Expression<Func<Products, bool>> filter, Func<IQueryable<Products>, IOrderedQueryable<Products>> orderby = null, params Expression<Func<Products, object>>[] includes);
        Task<List<ProductViewModel>> GetAll();
        Task<int> GetCountByCategory(int id);
    }
}
