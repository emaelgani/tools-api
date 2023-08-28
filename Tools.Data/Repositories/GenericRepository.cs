using Tools.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tools.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _dbcontext { get; set; }
        private DbSet<T> _entitySet
        {
            get
            {
                return _dbcontext.Set<T>();
            }
        }

        public GenericRepository(DbContext db) => _dbcontext = db;

        public IQueryable<T> Entity => _entitySet.AsQueryable();

        public IQueryable<T> EntityNoTracking => _entitySet.AsNoTracking();

        public async Task<List<T>> GetAllAsync() { return await _entitySet.ToListAsync<T>(); }

        public void Add(T entity) => _entitySet.Add(entity);

        public void AddRange(IEnumerable<T> entities) => _entitySet.AddRange(entities);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition) => _entitySet.Where(condition);

        public async ValueTask<T?> FindByIdAsync(params object[] pkeys) => await _entitySet.FindAsync(pkeys);

        public void Remove(T entity) => _entitySet.Remove(entity);

        public async Task RemoveByIdAsync(params object[] pkeys)
        {
            T? entity = await FindByIdAsync(pkeys);
            if (entity != null)
                _entitySet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities) => _entitySet.RemoveRange(entities);

        public void Update(T entity) => _entitySet.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => _entitySet.UpdateRange(entities);

        public Task<int> CommitAsync() => _dbcontext.SaveChangesAsync();

        public int Commit() => _dbcontext.SaveChanges();

    }
}
