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

            #region Task
            modelBuilder.Entity<Core.Entities.Task>()
               .HasOne(t => t.AssignedToUser)
               .WithMany(u => u.AssignedTasks)
               .HasForeignKey(t => t.AssignedToUserId)
               .OnDelete(DeleteBehavior.Restrict);

            // Task - CreatedByUser (Many-to-One)
            modelBuilder.Entity<Core.Entities.Task>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task - Team (Many-to-One)
            modelBuilder.Entity<Core.Entities.Task>()
                .HasOne(t => t.Team)
                .WithMany()
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Team
            // Self-referencing Team Relationship
            modelBuilder.Entity<Team>()
                .HasOne(t => t.AssignedTeam)
                .WithMany()
                .HasForeignKey(t => t.AssignedTeamId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Team - AssignedToUser
            modelBuilder.Entity<Team>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTeams)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team - CreatedByUser
            modelBuilder.Entity<Team>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTeams)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            


            // seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Admin User", Email = "admin@demo.com", Role = "Admin" },
                new User { Id = 2, FullName = "Manager User", Email = "manager@demo.com", Role = "Manager" },
                new User { Id = 3, FullName = "Employee User", Email = "employee@demo.com", Role = "Employee" }
            );
        }
    }
}
