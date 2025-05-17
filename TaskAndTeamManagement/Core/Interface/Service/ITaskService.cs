using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;
using TaskAndTeamManagement.Core.Entities;
using Task = TaskAndTeamManagement.Core.Entities.Task;

namespace TaskAndTeamManagement.Core.Interface.Service
{
    public interface ITaskService : IGenericRepository<Task>
    {
    }
}
