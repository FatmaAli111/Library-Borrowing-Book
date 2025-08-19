using DataAcess.Repos.IRepos;
using Infrastructure.Repos.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}