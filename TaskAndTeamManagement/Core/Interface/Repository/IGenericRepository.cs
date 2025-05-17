using TaskAndTeamManagement.Core.Interface.ISpecification;

namespace TaskAndTeamManagement.Core.Interface.Repository
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> 
        {
            Task<T> GetByIdAsync(int id);
            //Task<T> GetByIdWithIncludeAsync(string id, params Expression<Func<T, object>>[] includeProperties);
            //Task<T> GetByIdWithoutTrackingAsync(int id);
            Task<T> GetEntityWithSpec(ISpecification<T> spec);
            Task<IReadOnlyList<T>> GetAllListAsync(ISpecification<T> spec);
            
            Task<int> CountAsync(ISpecification<T> spec);
            Task<bool> AddDataAsync(T entity);
            Task<bool> UpdateDataAsync(T entity);
            Task<bool> DeleteAsync(int id);
            //Task<bool> DeleteMultipleAsync(IEnumerable<int> ids);
        }
    }
}
