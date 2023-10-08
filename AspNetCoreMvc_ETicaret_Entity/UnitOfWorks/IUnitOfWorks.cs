using AspNetCoreMvc_ETicaret_Entity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.UnitOfWorks
{
    public interface IUnitOfWorks : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        void Commit();      //içine SaveChanges() gelecek.
        Task CommitAsync();
    }
}
