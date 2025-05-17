using Dapper;
using System.Data;
using TaskAndTeamManagement.Core.Interface.Repository;
using TaskAndTeamManagement.Infrascture.Data;

namespace TaskAndTeamManagement.Infrascture.Implementation.Repo
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _context;

        public DapperRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<(T1 ResultSet1, ICollection<T2> ResultSet2)> ExecuteStoredProcedureWithMultipleResultsAsync<T1, T2>(string storedProcedure, object? parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    using (var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
                    {
                        // First result set (single value)
                        var resultSet1 = await multi.ReadSingleAsync<T1>();

                        // Second result set
                        var resultSet2 = (await multi.ReadAsync<T2>()).ToList();

                        return (resultSet1, resultSet2);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while executing the stored procedure.", ex);
            }
        }

        public async Task<(ICollection<T1> ResultSet1, ICollection<T2> ResultSet2, ICollection<T3> ResultSet3)> ExecuteStoredProcedureWithTripleResultSetAsync<T1, T2, T3>(string storedProcedure, object? parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    using (var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
                    {

                        var resultSet1 = (await multi.ReadAsync<T1>()).ToList();
                        var resultSet2 = (await multi.ReadAsync<T2>()).ToList();
                        var resultSet3 = (await multi.ReadAsync<T3>()).ToList();

                        return (resultSet1, resultSet2, resultSet3);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while executing the stored procedure.", ex);
            }
        }

        public async Task<ICollection<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object? parameters = null)
        {
            try
            {

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<TResult>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                    return result?.ToList() ?? new List<TResult>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during get all filterd data.", ex);
            }
        }

        public async Task<TResult> ExecuteSPForSingleRowResultAsync<TResult>(string storedProcedure)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<TResult>(storedProcedure, commandType: CommandType.StoredProcedure);
                    if (result == null)
                    {
                        throw new Exception($"No data was returned from the stored procedure: {storedProcedure}");
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during get all filterd data.", ex);
            }
        }

        public async Task<bool> ExecuteQueryCommand(string sql, object parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.ExecuteAsync(sql, parameters) > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during execute query", ex); ;
            }

        }
        public async Task<int> ExecuteQueryScalarValueAsync(string sql, object parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.ExecuteScalarAsync<int>(sql, parameters);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during execute query", ex); ;
            }

        }
        public async Task<ICollection<TResult>> ExecuteQuery<TResult>(string sql, object parameters = null)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<TResult>(sql, parameters);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during execute query", ex); ;
            }

        }

    }
}
