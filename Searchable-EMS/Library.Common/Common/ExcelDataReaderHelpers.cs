using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class ExcelDataReaderHelpers
    {
        string _path;
        public ExcelDataReaderHelpers(string path)
        {
            _path = path;
        }
        public IExcelDataReader getExcelReader()
        {
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = null;
            try
            {
                if (_path.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (_path.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (_path.EndsWith(".csv"))
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.GetEncoding(1252),
                        AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' }
                    });
                }
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DataRow> getData()
        {
            var reader = this.getExcelReader();
            var excelSheet = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            var sheetsName = from DataTable sheets in excelSheet.Tables select sheets.TableName;
            string sheetName = sheetsName.Take(1).FirstOrDefault().ToString();

            var workSheet = reader.AsDataSet().Tables[sheetName];

            if (workSheet.Rows.Count > 0)
            {
                DataRow rowZero = workSheet.Rows[0];
                //DataRow rowOne = workSheet.Rows[1];

                workSheet.Rows.Remove(rowZero);
                // workSheet.Rows.Remove(rowOne);

                var rows = from DataRow a in workSheet.Rows select a;

                reader.Close();
                return rows;
            }
            else
            {
                reader.Close();
                return null;
            }

        }



        public IExcelDataReader ConvertCSVtoDataTable(FileStream stream)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return (IExcelDataReader)dt;
        }
    }
}
