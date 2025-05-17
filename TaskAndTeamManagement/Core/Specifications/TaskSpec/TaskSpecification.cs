namespace TaskAndTeamManagement.Core.Specifications.TaskSpec
{
    public class TaskSpecification : BaseSpecification<Core.Entities.Task>
    {
        public TaskSpecification(TaskSpecParams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) ||
                 x.Title.ToLower().Contains(specParams.Search.ToLower())) &&
                (string.IsNullOrEmpty(specParams.Status) || x.Status == specParams.Status) &&
                (!specParams.AssignedToUserId.HasValue || x.AssignedToUserId == specParams.AssignedToUserId) &&
                (!specParams.TeamId.HasValue || x.TeamId == specParams.TeamId) &&
                (!specParams.DueDate.HasValue || x.DueDate.Value.Date == specParams.DueDate.Value.Date)
            )
        {
            AddInclude(x => x.AssignedToUser);
            AddInclude(x => x.CreatedByUser);
            AddInclude(x => x.Team);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "titleAsc":
                        AddOrderBy(x => x.Title);
                        break;
                    case "titleDesc":
                        AddOrderByDescending(x => x.Title);
                        break;
                    case "dueDateAsc":
                        AddOrderBy(x => x.DueDate);
                        break;
                    case "dueDateDesc":
                        AddOrderByDescending(x => x.DueDate);
                        break;
                    default:
                        AddOrderByDescending(x => x.Id);
                        break;
                }
            }
        }
    }
}
