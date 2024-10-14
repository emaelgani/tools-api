using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class CachedProveedorRepository : IProveedorRepository
    {

        private readonly IProveedorRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CachedProveedorRepository(IProveedorRepository decorated, IMemoryCache memory)
        {
            _decorated = decorated;
            _memoryCache = memory;
        }

        public IQueryable<Proveedor> Entity => _decorated.Entity;

        public IQueryable<Proveedor> EntityNoTracking => _decorated.EntityNoTracking;

        public void Add(Proveedor entity)
        {
            _decorated.Add(entity);
        }

        public void AddRange(IEnumerable<Proveedor> entities)
        {
            _decorated.AddRange(entities);
        }

        public int Commit()
        {
            return _decorated.Commit();
        }

        public Task<int> CommitAsync()
        {
            return _decorated.CommitAsync();
        }

        public IQueryable<Proveedor> FindByCondition(Expression<Func<Proveedor, bool>> condition)
        {
            return _decorated.FindByCondition(condition);
        }

        public ValueTask<Proveedor?> FindByIdAsync(params object[] pkeys)
        {
            return _decorated.FindByIdAsync(pkeys);
        }

        public Task<List<Proveedor>> GetAllAsync()
        {
            return _decorated.GetAllAsync();
        }

        public Task<IList<Proveedor>?> GetAllProveedorWithProductsAsync()
        {

            string key = "getAllProveedorWithProducts";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2)); //Expira en 2 minutos

                    return _decorated.GetAllProveedorWithProductsAsync();

                }
            );
        }

        public Task<Proveedor?> GetProveedorWithProductsAsync(int id)
        {
            return _decorated.GetProveedorWithProductsAsync(id);
        }

        public void Remove(Proveedor entity)
        {
            _decorated.Remove(entity);
        }

        public Task RemoveByIdAsync(params object[] pkeys)
        {
            return _decorated.RemoveByIdAsync(pkeys);
        }

        public void RemoveRange(IEnumerable<Proveedor> entities)
        {
            _decorated.RemoveRange(entities);
        }

        public void Update(Proveedor entity)
        {
            _decorated.Update(entity);
        }

        public void UpdateRange(IEnumerable<Proveedor> entities)
        {
            _decorated.UpdateRange(entities);
        }
    }
}
