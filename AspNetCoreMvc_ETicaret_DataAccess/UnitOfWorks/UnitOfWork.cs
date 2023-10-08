using AspNetCoreMvc_ETicaret_DataAccess.Context;
using AspNetCoreMvc_ETicaret_DataAccess.Repositories;
using AspNetCoreMvc_ETicaret_Entity.Repositories;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWorks
    {
        private readonly ECommerceDbContext _context;
        private bool disposed = false;

        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
