using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;
using TaskAndTeamManagement.Core.Entities;

namespace TaskAndTeamManagement.Core.Interface.Service
{
    public interface ITeamService : IGenericRepository<Team>
    {
    }
}
