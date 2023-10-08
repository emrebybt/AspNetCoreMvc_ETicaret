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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private readonly IProductSpecsService _productSpecsService;

        public ProductService(IUnitOfWorks uow, IMapper mapper, IProductSpecsService productSpecsService)
        {
            _uow = uow;
            _mapper = mapper;
            _productSpecsService = productSpecsService;
        }
        public async Task Add(ProductViewModel model)
        {
            await _uow.GetRepository<Products>().Add(_mapper.Map<Products>(model));
            await _uow.CommitAsync();
        }

        public void Delete(ProductViewModel model)
        {
            _uow.GetRepository<Products>().Delete(_mapper.Map<Products>(model));
            _uow.Commit();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var list = await _uow.GetRepository<Products>().GetAllAsync();
            return _mapper.Map<List<ProductViewModel>>(list);
        }

        public List<ProductViewModel> GetAllByFilterNoAsync(Expression<Func<Products, bool>> filter, Func<IQueryable<Products>, IOrderedQueryable<Products>> orderby = null, params Expression<Func<Products, object>>[] includes)
        {
            var list = _uow.GetRepository<Products>().GetAllNotAsync(filter, orderby, includes);
            return _mapper.Map<List<ProductViewModel>>(list);
        }

        public async Task<ProductViewModel> GetByFilter(Expression<Func<Products, bool>> filter, params Expression<Func<Products, object>>[] includes)
        {
            var list = await _uow.GetRepository<Products>().Get(filter,null,includes);
            return _mapper.Map<ProductViewModel>(list);
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var product = await _uow.GetRepository<Products>().GetById(id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public ProductViewModel GetByIdNotAsync(int id)
        {
            var product = _uow.GetRepository<Products>().GetNotAsync(x=>x.Id == id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<int> GetCountByCategory(int id)
        {
            var list = await _uow.GetRepository<Products>().GetAll(x=>x.CategoryId == id);
            return list.Count();
        }

        public async Task<IEnumerable<ProductViewModel>> GetListAllByFilter(Expression<Func<Products, bool>> filter, params Expression<Func<Products, object>>[] includes)
        {
            var list = await _uow.GetRepository<Products>().GetAll(filter, null, includes);
            return _mapper.Map<List<ProductViewModel>>(list);
        }

        public async Task<List<ProductViewModel>> GetProductsBySpecs(List<ProductViewModel> model, int? id, string[]? value)
        {
            var specs = await _productSpecsService.GetListAllByFilter(x => x.Product.CategoryId == id);
            List<ProductViewModel> list = new List<ProductViewModel>();
            foreach (var item in value)
            {
                foreach (var spec in specs)
                {
                    if (spec.Key == item.Split("-")[0] && spec.Value == item.Split("-")[1])
                    {
                        if (list.Contains(model.Where(x => x.Id == spec.ProductId).FirstOrDefault()))
                        {
                            continue;
                        }
                        else
                        {
                            list.Add(model.Where(x => x.Id == spec.ProductId).FirstOrDefault());
                        }

                    }
                }
            }
            return list;
        }

        public void Update(ProductViewModel model)
        {
            _uow.GetRepository<Products>().Update(_mapper.Map<Products>(model));
            _uow.Commit();
        }
    }
}
