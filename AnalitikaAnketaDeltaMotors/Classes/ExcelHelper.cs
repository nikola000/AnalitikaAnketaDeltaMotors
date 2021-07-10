using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnalitikaAnketaDeltaMotors.Classes
{
    public class ExcelHelper
    {
        public string FilePath { get; set; }
        public System.Data.DataTable Data { get; set; }
        public ExcelHelper()
        {

        }
        public System.Data.DataTable ImportData(ProgressBar progressBar)
        {
            if (FilePath.Trim() == "")
            {
                return null;
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Open(FilePath);
            Sheets excelSheets = wb.Worksheets;
            Worksheet sheet = (Worksheet)excelSheets[1];
            Range xlRange = sheet.UsedRange;
            int colCount = xlRange.Columns.Count;
            double progressValue = progressBar.Value;
            int rowCount = 0;
            for (int i = 2; i < xlRange.Rows.Count; i++)
            {
                try
                {
                    sheet.Cells[i, 1].Value.ToString();
                    rowCount++;
                }
                catch (Exception)
                {
                    break;
                }                
            }
            for (int i = 1; i < 2; i++)
            {              
                for (int j = 1; j < colCount; j++)
                {
                    try
                    {
                        dt.Columns.Add(sheet.Cells[i, j].Value.ToString());
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }     
            for (int i = 2; i <= rowCount+1; i++)
            {
                DataRow newRow = dt.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    try
                    {
                        newRow[j-1] = sheet.Cells[i, j].Value.ToString();
                    }
                    catch (Exception)
                    {
                        continue;                      
                    }
                }
                progressValue += 100.0 / rowCount;
                if (progressValue > 100)
                    progressValue = 100;
                progressBar.Value = (int)Math.Ceiling(progressValue);
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}
