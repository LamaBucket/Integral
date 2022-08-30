using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.Domain.Services
{
    public class CSVDataTableParser : IDataParserService<string, DataTable>
    {
        public DataTable? Parse(string item)
        {
            string[] rows = item.Split(Environment.NewLine);

            if (rows.Length == 0)
                return null;

            IEnumerable<string> columns = rows[0].Split(",");

            DataTable dt = new();

            foreach(string column in columns)
            {
                dt.Columns.Add(column, typeof(object));
            }

            for(int i = 1; i < rows.Length; i++)
            {
                dt.Rows.Add(rows[i].Split(","));
            }

            return dt;
        }

        public string? ParseBack(DataTable item)
        {
            StringBuilder sb = new StringBuilder();

            string[] columnNames = item.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();

            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in item.Rows)
            {
                string[]? fields = row.ItemArray?.Select(field => field?.ToString() ?? String.Empty)?.ToArray();
                if(fields is not null)
                {
                    sb.AppendLine(string.Join(",", fields));
                }
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}
