using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.RepositoryContract;
using OrderManagementSystem.Infrastructure.Context;
using System.Linq.Expressions;

namespace OrderManagementSystem.Infrastructure.RepositoryImplementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        public IQueryable<T> GetAllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }
        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }
        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool asTracking = false)
        {
            var query = _dbSet.Where(expression);
            return asTracking ? query : query.AsNoTracking();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}