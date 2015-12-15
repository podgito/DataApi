using System.Collections.Generic;
using System.Data;

namespace DataApi.Internals
{
    public interface ISQLDataSource 
    {
        DataTable ExecuteQuery(string query);
        DataTable ExecuteQuery(string query, Dictionary<string, object> inputParameters);
    }
}