using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DataProviders
{
    public class SqlServerDataProvider : IDataProvider
    {
        private string _connectionString;

        public SqlServerDataProvider()
        {

        }

        public SqlServerDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetDatTable(string storedProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                var dataTable = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    try
                    {
                        // this will query your database and return the result to your datatable
                        da.Fill(dataTable);
                    }
                    finally
                    {
                        // Always call Close when done reading
                    }
                    return dataTable;
                }
            }
        }

        void Execute()
        {
            string queryString = "SELECT tPatCulIntPatIDPk, tPatSFirstname, tPatSName, tPatDBirthday  FROM  [dbo].[TPatientRaw] WHERE tPatSName = @tPatSName";
            //string connectionString = @"Server=.\PDATA_SQLEXPRESS;Database=;User Id=sa;Password=2BeChanged!;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}",
                        reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }


        public void ExecuteStoredProcedure(string storedProcedure, IDictionary<string, object> inputs)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string storedProcedure, IDictionary<string, object> inputs)
        {
            throw new NotImplementedException();
        }
    }
}
