using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.Internals.Data.SQLServer
{
    public class SQLServerClass
    {
        private string _connectionString;

        public SQLServerClass(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection sqlConnection1 = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet resultSet = new DataSet();
            var sqlAdapter = new SqlDataAdapter(cmd);

            cmd.CommandText = "SELECT * FROM Products";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            sqlAdapter.Fill(resultSet);


            // Data is accessible through the DataReader object here.

            sqlConnection1.Close();

            return resultSet.Tables[0];
        }

        public DataTable ExecuteQueryStoredProcedure(string storedProcedureName, Dictionary<string, object> inputs)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, Dictionary<string, object> inputs)
        {
            throw new NotImplementedException();
        }

    }
}
