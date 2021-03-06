﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.Internals
{
    static class DataApiConstants
    {
        public static string DataApiControllerName = "DataApi";
        public static string ControllerKey = "controller";
        public static string QueryKey = "query";
        public static string MappingFunctionKey = "mappingFunction";
        public static string QueryBindingKey = "queryBinding";

        public static string DataSourceKey = "dataSource";

        public static string GetWithoutMappingErrorMessage = "No mapping setup to Get data.";
    }
}
;