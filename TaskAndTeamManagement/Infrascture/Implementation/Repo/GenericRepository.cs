using Microsoft.EntityFrameworkCore;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;
using System.Linq.Expressions;
using TaskAndTeamManagement.Core.Interface.ISpecification;
using TaskAndTeamManagement.Infrascture.Data;

namespace TaskAndTeamManagement.Infrascture.Implementation.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TaskDbContext _dataContext;
        public GenericRepository(TaskDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithIncludeAsync(string id, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dataContext.Set<T>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during get data.");
            }

        }


        //public async Task<T> GetByIdWithoutTrackingAsync(int id)
        //{
        //    return await _dataContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        //}
        public async Task<IReadOnlyList<T>> GetAllListAsync(ISpecification<T> spec) 
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) 
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        public async Task<bool> AddDataAsync(T entity)
        {
            try
            {
                _dataContext.Set<T>().Add(entity);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> UpdateDataAsync(T entity)
        {
            _dataContext.Set<T>().Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = await _dataContext.Set<T>().FindAsync(id);
                _dataContext.Set<T>().Remove(data);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                if (IsForeignKeyConstraintViolation(ex))
                {
                    throw new Exception("Some entities are referenced by others and cannot be deleted.");
                }
                throw;
            }


        }


        //public async Task<bool> DeleteMultipleAsync(IEnumerable<int> ids)
        //{
        //    var entities = await _dataContext.Set<T>().Where(e => ids.Contains(e.id)).ToListAsync();
        //    if (entities == null || !entities.Any()) return false;

        //    _dataContext.Set<T>().RemoveRange(entities);
        //    return await SaveAsync();
        //}



        #region Private Methods

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var result = SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), spec);
            return result;
        }


        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private bool IsForeignKeyConstraintViolation(Exception exception)
        {
            var innerException = exception;

            // Traverse exception hierarchy
            while (innerException != null)
            {
                if (innerException.Message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase) ||
                    innerException.Message.Contains("FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                innerException = innerException.InnerException;
            }

            return false;
        }



        #endregion
    }
}
