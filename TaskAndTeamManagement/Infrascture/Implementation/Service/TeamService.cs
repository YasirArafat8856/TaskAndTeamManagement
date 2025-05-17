using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.ISpecification;
using TaskAndTeamManagement.Core.Interface.Service;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

namespace TaskAndTeamManagement.Infrascture.Implementation.Service
{
    public class TeamService(IGenericRepository<Team> _teamRepo) : ITeamService
    {
        public async Task<Team> GetByIdAsync(int id) => await _teamRepo.GetByIdAsync(id);
        public async Task<IReadOnlyList<Team>> GetAllListAsync(ISpecification<Team> spec) => await _teamRepo.GetAllListAsync(spec);
        public async Task<bool> AddDataAsync(Team team) => await _teamRepo.AddDataAsync(team);
        public async Task<bool> UpdateDataAsync(Team team) => await _teamRepo.UpdateDataAsync(team);
        public async Task<bool> DeleteAsync(int id) => await _teamRepo.DeleteAsync(id);

        public async Task<int> CountAsync(ISpecification<Team> spec)
        {
            return await _teamRepo.CountAsync(spec);
        }


        public async Task<Team> GetEntityWithSpec(ISpecification<Team> spec)
        {
            return await _teamRepo.GetEntityWithSpec(spec);
        }

    }
}
