using AutoMapper;
using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.DTO.Notification;
using TaskAndTeamManagement.DTO.Task;
using TaskAndTeamManagement.DTO.Team;
using TaskAndTeamManagement.DTO.User;
using Task = TaskAndTeamManagement.Core.Entities.Task;

namespace TaskAndTeamManagement.Infrascture.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<User, ReturnUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();

            // Task Mappings
            CreateMap<Task, AddTaskDto>().ReverseMap();
            CreateMap<Task, ReturnTaskDto>().ReverseMap();
            CreateMap<Task, UpdateTaskDto>().ReverseMap();

            // Team Mappings
            CreateMap<Team, AddTeamDto>().ReverseMap();
            CreateMap<Team, ReturnTeamDto>().ReverseMap();
            CreateMap<Team, UpdateTeamDto>().ReverseMap();

            // Notification Mappings
            //CreateMap<Notification, AddNotificationDto>().ReverseMap();
            CreateMap<Notification, ReturnNotificationDto>().ReverseMap();
            //CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
        }
    }
}
