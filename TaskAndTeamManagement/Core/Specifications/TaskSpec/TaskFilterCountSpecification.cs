namespace TaskAndTeamManagement.Core.Specifications.TaskSpec
{
    public class TaskFilterCountSpecification : BaseSpecification<Core.Entities.Task>
    {
        public TaskFilterCountSpecification(TaskSpecParams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) ||
                 x.Title.ToLower().Contains(specParams.Search.ToLower())) &&
                (string.IsNullOrEmpty(specParams.Status) || x.Status == specParams.Status) &&
                (!specParams.AssignedToUserId.HasValue || x.AssignedToUserId == specParams.AssignedToUserId) &&
                (!specParams.TeamId.HasValue || x.TeamId == specParams.TeamId) &&
                (!specParams.DueDate.HasValue || x.DueDate.Value.Date == specParams.DueDate.Value.Date)
            )
        { }
    }
}
