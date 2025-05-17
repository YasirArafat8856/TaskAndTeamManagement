namespace TaskAndTeamManagement.Core.Interface.UnitOFWork
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
