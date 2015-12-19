using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.Internals
{
    public static class DictionaryExtensions
    {

        public static IEnumerable<SqlParameter> ToSQLParameters(this IDictionary<string, object> dictionary)
        {
            return ToSQLParameters(dictionary, s => s);
        }

        public static IEnumerable<SqlParameter> ToSQLParameters(this IDictionary<string, object> dictionary, Func<string, string> sqlParameterNamingConversion)
        {
            foreach (var kvp in dictionary)
            {
                var convertedKey = sqlParameterNamingConversion(kvp.Key);

                yield return new SqlParameter(convertedKey, kvp.Value);
            }
        }


    }
}
