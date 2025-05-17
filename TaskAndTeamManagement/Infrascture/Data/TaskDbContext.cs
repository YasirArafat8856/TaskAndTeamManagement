using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagement.Core.Entities;

namespace TaskAndTeamManagement.Infrascture.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Core.Entities.Task> Tasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Admin User", Email = "admin@demo.com", Role = "Admin" },
                new User { Id = 2, FullName = "Manager User", Email = "manager@demo.com", Role = "Manager" },
                new User { Id = 3, FullName = "Employee User", Email = "employee@demo.com", Role = "Employee" }
            );
        }
    }
}
