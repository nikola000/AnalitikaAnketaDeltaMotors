using System;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.Classes;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Import : Form
    {
        ExcelHelper helper;
        public Import()
        {
            InitializeComponent();
            helper = new ExcelHelper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFD.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                txtIzborFajla.Text = openFD.FileName;
            }
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {
            helper.FilePath = txtIzborFajla.Text;
            helper.ImportData();

            //string filePath = txtIzborFajla.Text;
            ////string fileExt = Path.GetExtension(filePath);
            ////DataTable dtExcel = new DataTable();
            ////dtExcel = ReadExcel(filePath, fileExt);//read excel file
            ////dataGridView1.Visible = true;
            ////dataGridView1.DataSource = dtExcel;

            ////Create COM Objects.
            //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //DataRow myNewRow;
            //DataTable myTable;


            //if (excelApp == null)
            //{
            //    Console.WriteLine("Excel is not installed!!");
            //    return;
            //}

            ////
            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(@"C:\Users\Nikola\Desktop\SAMPLE_1-364484958.xls");
            //Microsoft.Office.Interop.Excel._Worksheet excelSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelBook.Sheets[1];
            //Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            //int rows = excelRange.Rows.Count;
            //int cols = excelRange.Columns.Count;

            ////Set DataTable Name and Columns Name
            //myTable = new DataTable("MyDataTable");
            //myTable.Columns.Add("FirstName", typeof(string));
            //myTable.Columns.Add("LastName", typeof(string));
            //myTable.Columns.Add("Age", typeof(int));



            ////first row using for heading, start second row for data
            //for (int i = 2; i <= rows; i++)
            //{
            //    myNewRow = myTable.NewRow();
            //    myNewRow["FirstName"] = excelRange.Cells[i, 1].values2.ToString(); //string
            //    myNewRow["LastName"] = excelRange.Cells[i, 2].ToString(); //string
            //    myNewRow["Age"] = Convert.ToInt32(excelRange.Cells[i, 3].ToString()); //integer

            //    myTable.Rows.Add(myNewRow);
            //}
            //dataGridView1.DataSource = myTable;



            ////after reading, relaase the excel project
            //excelApp.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            //Console.ReadLine();

            /////////
        }
        
    }
}

