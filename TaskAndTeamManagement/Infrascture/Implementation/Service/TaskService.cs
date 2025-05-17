using TaskAndTeamManagement.Core.Interface.ISpecification;
using TaskAndTeamManagement.Core.Interface.Service;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

namespace TaskAndTeamManagement.Infrascture.Implementation.Service
{
    public class TaskService(IGenericRepository<Core.Entities.Task> _taskRepo) : ITaskService
    {
        public async Task<Core.Entities.Task> GetByIdAsync(int id) => await _taskRepo.GetByIdAsync(id);
        public async Task<IReadOnlyList<Core.Entities.Task>> GetAllListAsync(ISpecification<Core.Entities.Task> spec) => await _taskRepo.GetAllListAsync(spec);
        public async Task<bool> AddDataAsync(Core.Entities.Task task) => await _taskRepo.AddDataAsync(task);
        public async Task<bool> UpdateDataAsync(Core.Entities.Task task) => await _taskRepo.UpdateDataAsync(task);
        public async Task<bool> DeleteAsync(int id) => await _taskRepo.DeleteAsync(id);


        public async Task<int> CountAsync(ISpecification<Core.Entities.Task> spec)
        {
            return await _taskRepo.CountAsync(spec);
        }

        public async Task<Core.Entities.Task> GetEntityWithSpec(ISpecification<Core.Entities.Task> spec)
        {
            return await _taskRepo.GetEntityWithSpec(spec);
        }

    }
}
