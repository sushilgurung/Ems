﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class CsvExport
    {
        /// <summary>
        /// To keep the ordered list of column names
        /// </summary>
        List<string> fields = new List<string>();

        /// <summary>
        /// The list of rows
        /// </summary>
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        /// <summary>
        /// The current row
        /// </summary>
        Dictionary<string, object> currentRow { get { return rows[rows.Count - 1]; } }

        /// <summary>
        /// Set a value on this column
        /// </summary>
        public object this[string field]
        {
            set
            {
                // Keep track of the field names, because the dictionary loses the ordering
                if (!fields.Contains(field)) fields.Add(field);
                currentRow[field] = value;
            }
        }

        /// <summary>
        /// Call this before setting any fields on a row
        /// </summary>
        public void AddRow()
        {
            rows.Add(new Dictionary<string, object>());
        }

        /// <summary>
        /// Add a list of typed objects, maps object properties to CsvFields
        /// </summary>
        public void AddRows<T>(IEnumerable<T> list)
        {
            if (list.Any())
            {
                foreach (var obj in list)
                {
                    AddRow();
                    var values = obj.GetType().GetProperties();
                    foreach (var value in values)
                    {
                        this[value.Name] = value.GetValue(obj, null);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";
            if (value is INullable && ((INullable)value).IsNull) return "";
            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();
            if (output.Contains(",") || output.Contains("\"") || output.Contains("\n") || output.Contains("\r"))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            if (output.Length > 30000) //cropping value for stupid Excel
            {
                if (output.EndsWith("\""))
                    output = output.Substring(0, 30000) + "\"";
                else
                    output = output.Substring(0, 30000);
            }

            return output.Length <= 32767 ? output : output.Substring(0, 32767);
        }

        /// <summary>
        /// Output all rows as a CSV returning a string
        /// </summary>
        public string Export()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("sep=,");

            // The header
            sb.Append(string.Join(",", fields.ToArray()));
            sb.AppendLine();

            // The rows
            foreach (Dictionary<string, object> row in rows)
            {
                fields.Where(f => !row.ContainsKey(f)).ToList().ForEach(k =>
                {
                    row[k] = null;
                });
                sb.Append(string.Join(",", fields.Select(field => MakeValueCsvFriendly(row[field])).ToArray()));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Exports to a file
        /// </summary>
        public void ExportToFile(string path)
        {
            File.WriteAllText(path, Export(), Encoding.Default);
        }

        /// <summary>
        /// Exports as raw UTF8 bytes
        /// </summary>
        public byte[] ExportToBytes()
        {
            var data = Encoding.UTF8.GetBytes(Export());
            return Encoding.UTF8.GetPreamble().Concat(data).ToArray();
        }
    }
}
