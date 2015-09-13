using System;
using System.Collections.Generic;
using System.Data;
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
