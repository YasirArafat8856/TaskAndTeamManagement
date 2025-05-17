using TaskAndTeamManagement.Core.Entities;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

namespace TaskAndTeamManagement.Core.Interface.Service
{
    public interface IUserService : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string emil); 
    }
}
