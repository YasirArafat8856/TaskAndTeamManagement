using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagement.Core.Interface.UnitOFWork;
using TaskAndTeamManagement.Infrascture.Data;

namespace TaskAndTeamManagement.Infrascture.Implementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
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
            _dbContext.Dispose();
        }
    }
}
