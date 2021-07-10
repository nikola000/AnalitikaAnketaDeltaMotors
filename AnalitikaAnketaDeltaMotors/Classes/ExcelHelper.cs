using Microsoft.Office.Interop.Excel;
using System;
using System.Data;

namespace AnalitikaAnketaDeltaMotors.Classes
{
    public class ExcelHelper
    {
        public string FilePath { get; set; }
        public System.Data.DataTable Data { get; set; }
        public ExcelHelper()
        {

        }

        public System.Data.DataTable ImportData()
        {
            if (FilePath.Trim() == "")
            {
                return null;
            }
            System.Data.DataTable dt = new System.Data.DataTable();           
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(FilePath);
            Sheets excelSheets = wb.Worksheets;
            Worksheet sheet = (Worksheet)excelSheets[1];
            Range xlRange = sheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            for (int i = 1; i < 2; i++)
            {              
                for (int j = 1; j < colCount; j++)
                {
                    try
                    {
                        DataColumn column = new DataColumn();
                        column.ColumnName = sheet.Cells[i, j].Value.ToString();
                        dt.Columns.Add(sheet.Cells[i, j].Value.ToString());
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            int emptyCell = 0;
            for (int i = 2; i <= rowCount; i++)
            {
                DataRow newRow = dt.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    try
                    {
                        newRow[j-1] = sheet.Cells[i, j].Value.ToString();
                        emptyCell = 0;
                    }
                    catch (Exception)
                    {
                        emptyCell++;
                        if (emptyCell== colCount-4)
                        {
                            break;
                        }
                        continue;                      
                    }
                }
                if (emptyCell > colCount-5)
                {
                    break;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}
