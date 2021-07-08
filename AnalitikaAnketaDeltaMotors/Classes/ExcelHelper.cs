using Microsoft.Office.Interop.Excel;
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

        public void ImportData()
        {
            if (FilePath.Trim() == "")
            {
                return;
            }

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(FilePath);

            Microsoft.Office.Interop.Excel.Sheets excelSheets = wb.Worksheets;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets[1];
            var cell = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[2, 2];

            var cellValue = (excelWorksheet.Cells[2, 2] as Microsoft.Office.Interop.Excel.Range).Value;

            var sitovi = wb.Sheets;


        }
    }
}
