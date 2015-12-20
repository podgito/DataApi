using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.Internals.Data.SQLServer
{
    /// <summary>
    /// Data source for SqlServer databases
    /// </summary>
    public class SQLServerDataSource : ISQLDataSource
    {
        private string _connectionString;

        /// <summary>
        /// Create a SqlServer DataSource instance
        /// </summary>
        /// <param name="connectionString">Connection string for the database</param>
        public SQLServerDataSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes the query and returns a dataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query)
        {
            return ExecuteQuery(query, new Dictionary<string, object>());
        }

        /// <summary>
        /// Executes the query with input parameters and returns a dataTable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="inputParameters"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query, Dictionary<string, object> inputParameters)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet resultSet = new DataSet();
            var sqlAdapter = new SqlDataAdapter(cmd);

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            foreach(var kv in inputParameters)
            {
                cmd.Parameters.AddWithValue(kv.Key, kv.Value);
            }

            sqlConnection.Open();

            sqlAdapter.Fill(resultSet);


            // Data is accessible through the DataReader object here.

            sqlConnection.Close();

            return resultSet.Tables[0];
        }

        //public DataTable ExecuteQueryStoredProcedure(string storedProcedureName, Dictionary<string, object> inputs)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ExecuteNonQueryStoredProcedure(string storedProcedureName, Dictionary<string, object> inputs)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
