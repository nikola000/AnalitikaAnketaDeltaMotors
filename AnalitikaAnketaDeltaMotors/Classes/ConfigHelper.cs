using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalitikaAnketaDeltaMotors.Classes
{
    public static class ConfigHelper
    {
        public static string ReadConfigFile()
        {
            string value = "";
            using (FileStream fs = File.Open("config.db", FileMode.OpenOrCreate))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    value = temp.GetString(b);
                }
            }
            return value;
        }

        public static void WriteConfigFile(string value)
        {
            using (FileStream fs = File.Create("config.db"))
            {
                AddText(fs, value); 
            } 
        }

        public static (bool, string) CheckConnection(string connString)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                }
                Cursor.Current = Cursors.WaitCursor;
                return (true, "Konekcija uspesna");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.WaitCursor;
                return (false, "Konekcija neuspesna. Error: " + ex);
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
