namespace TaskAndTeamManagement.Core.Interface.Repository
{
    public interface IDapperRepository
    {
        Task<(T1 ResultSet1, ICollection<T2> ResultSet2)> ExecuteStoredProcedureWithMultipleResultsAsync<T1, T2>(string storedProcedure, object parameters = null);
        Task<(ICollection<T1> ResultSet1, ICollection<T2> ResultSet2, ICollection<T3> ResultSet3)> ExecuteStoredProcedureWithTripleResultSetAsync<T1, T2, T3>(string storedProcedure, object parameters = null);
        Task<ICollection<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters = null);
        Task<TResult> ExecuteSPForSingleRowResultAsync<TResult>(string storedProcedure);


        Task<bool> ExecuteQueryCommand(string sql, object parameters = null);
        Task<int> ExecuteQueryScalarValueAsync(string sql, object parameters = null);
        Task<ICollection<TResult>> ExecuteQuery<TResult>(string sql, object parameters = null);
    }
}
