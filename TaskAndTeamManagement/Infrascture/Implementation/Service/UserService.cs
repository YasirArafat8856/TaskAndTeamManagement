using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.ISpecification;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.Infrascture.Data;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

namespace TaskAndTeamManagement.Infrascture.Implementation.Service
{
    public class UserService(IGenericRepository<User> _userRepo, TaskDbContext _context) : IUserService
    {
        public async Task<User> GetByIdAsync(int id) => await _userRepo.GetByIdAsync(id);
        public async Task<IReadOnlyList<User>> GetAllListAsync(ISpecification<User> spec) => await _userRepo.GetAllListAsync(spec);
        public async Task<bool> AddDataAsync(User user) => await _userRepo.AddDataAsync(user);
        public async Task<bool> UpdateDataAsync(User user) => await _userRepo.UpdateDataAsync(user);
        public async Task<bool> DeleteAsync(int id) => await _userRepo.DeleteAsync(id);

        public async Task<User> GetEntityWithSpec(ISpecification<User> spec)
        {
            return await _userRepo.GetEntityWithSpec(spec);
        }
        public async Task<int> CountAsync(ISpecification<User> spec)
        {
            return await _userRepo.CountAsync(spec);
        }

        public async Task<User> GetByEmailAsync(string emil)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == emil);
        }
    }
}
