using System;
using System.Collections.Generic;
using System.Linq;

namespace DataApi.Internals.Data.SQL
{
    /// <summary>
    /// Class to parse out sql parameters from a SQL Command
    /// </summary>
    internal class SQLParameterResolver
    {
        /// <summary>
        /// Parses out any parameters using the @parameterName convention
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>The list of parameter names (without the leading '@')</returns>
        public IEnumerable<string> Resolve(string sql)
        {
            var words = sql.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return words.Where(w => w.StartsWith("@")).Select(w => w.Remove(0, 1)).Distinct();
        }
    }
}