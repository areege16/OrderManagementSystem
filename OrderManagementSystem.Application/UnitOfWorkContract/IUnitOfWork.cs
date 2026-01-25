using OrderManagementSystem.Application.RepositoryContract;

namespace OrderManagementSystem.Application.UnitOfWorkContract
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}