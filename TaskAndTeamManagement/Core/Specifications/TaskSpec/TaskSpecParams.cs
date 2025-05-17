namespace TaskAndTeamManagement.Core.Specifications.TaskSpec
{
    public class TaskSpecParams : EntitySpecParams
    {
        public string Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
