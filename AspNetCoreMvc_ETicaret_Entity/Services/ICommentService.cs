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
    public interface ICommentService
    {
        Task<List<CommentViewModel>> GetAllByFilter(Expression<Func<Comments, bool>> filter, Func<IQueryable<Comments>, IOrderedQueryable<Comments>> orderby = null, params Expression<Func<Comments, object>>[] includes);
        void Add(CommentViewModel model);
    }
}
