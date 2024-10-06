using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Cliente;

namespace Tools.Data.Repositories
{
    public class CachedClienteRepository : IClienteRepository
    {

        private readonly IClienteRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CachedClienteRepository(IClienteRepository decorated, IMemoryCache memory)
        {
            _decorated = decorated;
            _memoryCache = memory;
        }

        public IQueryable<Cliente> Entity => _decorated.Entity;

        public IQueryable<Cliente> EntityNoTracking => _decorated.EntityNoTracking;

        public void Add(Cliente entity)
        {
            _decorated.Add(entity);
        }

        public void AddRange(IEnumerable<Cliente> entities)
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

        public IQueryable<Cliente> FindByCondition(Expression<Func<Cliente, bool>> condition)
        {
            return _decorated.FindByCondition(condition);
        }

        public ValueTask<Cliente?> FindByIdAsync(params object[] pkeys)
        {
            return _decorated.FindByIdAsync(pkeys);
        }

        public Task<List<Cliente>> GetAllAsync()
        {
            string key = "all-clients";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2)); //Expira en 2 minutos

                    return _decorated.GetAllAsync();
                }
                );
        }

        public Task<IList<ClienteVentaProductoDTO>> GetClientesVentaByProducto(int IdProducto)
        {
            return _decorated.GetClientesVentaByProducto(IdProducto);
        }

        public void Remove(Cliente entity)
        {
           _decorated.Remove(entity);
        }

        public Task RemoveByIdAsync(params object[] pkeys)
        {
            return _decorated.RemoveByIdAsync(pkeys);
        }

        public void RemoveRange(IEnumerable<Cliente> entities)
        {
           _decorated.RemoveRange(entities);
        }

        public void Update(Cliente entity)
        {
           _decorated.Update(entity);
        }

        public void UpdateRange(IEnumerable<Cliente> entities)
        {
            _decorated.UpdateRange(entities);
        }
    }
}
