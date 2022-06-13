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
        
        public System.Data.DataTable ImportData(ref int maximum, System.Action<int> updateProgressBar)
        {
            maximum = 0;

            if (FilePath.Trim() == "")
            {
                return null;
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Open(FilePath);

            try
            {
                Sheets excelSheets = wb.Worksheets;
                Worksheet sheet = (Worksheet)excelSheets[1];
                Range xlRange = sheet.UsedRange;
                int colCount = 7;//xlRange.Columns.Count;

                int rowCount = 1;
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

                maximum = rowCount + 1;

                for (int j = 1; j < colCount; j++)
                {
                    try
                    {
                        dt.Columns.Add(sheet.Cells[1, j].Value.ToString());
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                for (int i = 2; i <= rowCount + 1; i++)
                {
                    DataRow newRow = dt.NewRow();
                    for (int j = 1; j <= colCount; j++)
                    {
                        try
                        {
                            newRow[j - 1] = sheet.Cells[i, j].Value.ToString();
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    updateProgressBar.Invoke(i);
                    dt.Rows.Add(newRow);
                }

                wb.Close();
            }
            catch (Exception e)
            {
                wb.Close();
                MessageBox.Show(e.Message, "Dogodila se greska");
                throw;
            }

            
            return dt;
        }
    }
}
