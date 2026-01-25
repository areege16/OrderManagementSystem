using OrderManagementSystem.Application.RepositoryContract;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Infrastructure.Context;
using OrderManagementSystem.Infrastructure.RepositoryImplementation;
using System.Collections.Concurrent;

namespace OrderManagementSystem.Infrastructure.UnitOfWorkImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            var repository = _repositories.GetOrAdd(type,
                _ => new GenericRepository<T>(_context));

            return (IGenericRepository<T>)repository;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}