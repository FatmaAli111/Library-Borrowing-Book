using DataAcess.Repos;
using DataAcess.Repos.IRepos;
using Infrastructure.Context;
using Infrastructure.Repos;
using Infrastructure.Repos.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public IBookRepository BookRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(AppDbContext context,
               IBookRepository bookRepository
             , IUserRepository userRepository, ICategoryRepository CategoryRepository)
        {
            _context = context;
            UserRepository =  userRepository;
            this.CategoryRepository = CategoryRepository;
            BookRepository =  bookRepository;

        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<T>(_context);
                _repositories[type] = repoInstance;
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _transaction?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}