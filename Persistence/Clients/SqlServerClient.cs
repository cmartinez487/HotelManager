using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Clients
{
    public class SqlServerClient
    {
        private readonly string _connectionString;

        public SqlServerClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> RetrieveSingleRowAsync<T>(string toExecute, DynamicParameters parameters, int? timeout, CommandType commandType = CommandType.Text)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<T>(toExecute, parameters, commandTimeout: timeout, commandType: commandType);
        }

        public async Task<IEnumerable<T>> RetrieveMultipleRowsAsync<T>(string toExecute,
            DynamicParameters parameters, int? timeout, CommandType commandType = CommandType.Text)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(toExecute, parameters, commandTimeout: timeout, commandType: commandType);
        }
    }
}
