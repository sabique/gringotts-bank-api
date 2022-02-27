using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DataTableExtension
    {
        public static T GetT<T>(this DataTable dataTable)
        {
            if (dataTable.Rows.Count < 1)
                return default(T);

            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            var objT = Activator.CreateInstance<T>();
            var row = dataTable.Rows[0];

            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name.ToLower()))
                {
                    pro.SetValue(objT, row[pro.Name]);
                }
            }

            return objT;
        }
    }
}
