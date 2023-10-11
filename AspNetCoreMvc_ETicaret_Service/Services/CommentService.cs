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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void Add(CommentViewModel model)
        {
            _uow.GetRepository<Comments>().Add(_mapper.Map<Comments>(model));
            _uow.Commit();
        }

        public async Task<List<CommentViewModel>> GetAllByFilter(Expression<Func<Comments, bool>> filter, Func<IQueryable<Comments>, IOrderedQueryable<Comments>> orderby = null, params Expression<Func<Comments, object>>[] includes)
        {
            var list = await _uow.GetRepository<Comments>().GetAll(filter,orderby,includes);
            return _mapper.Map<List<CommentViewModel>>(list);
        }
    }
}
