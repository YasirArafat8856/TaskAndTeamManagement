using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.ISpecification;
using TaskAndTeamManagement.Core.Interface.Service;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

namespace TaskAndTeamManagement.Infrascture.Implementation.Service
{
    public class NotificationService(IGenericRepository<Notification> _notificationRepo) : INotificationService
    {
        public async Task<Notification> GetByIdAsync(int id) => await _notificationRepo.GetByIdAsync(id);
        public async Task<IReadOnlyList<Notification>> GetAllListAsync(ISpecification<Notification> spec) => await _notificationRepo.GetAllListAsync(spec);
        public async Task<bool> AddDataAsync(Notification notification) => await _notificationRepo.AddDataAsync(notification);
        public async Task<bool> UpdateDataAsync(Notification notification) => await _notificationRepo.UpdateDataAsync(notification);
        public async Task<bool> DeleteAsync(int id) => await _notificationRepo.DeleteAsync(id);
        

        public async Task<int> CountAsync(ISpecification<Notification> spec)
        {
            return await _notificationRepo.CountAsync(spec);
        }

        public async Task<Notification> GetEntityWithSpec(ISpecification<Notification> spec)
        {
            return await _notificationRepo.GetEntityWithSpec(spec);
        }

    }
}
