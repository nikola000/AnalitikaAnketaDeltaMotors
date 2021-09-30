using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalitikaAnketaDeltaMotors
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            string myPassedInValue = this.Context.Parameters["conn"].Trim();
            string path = this.Context.Parameters["path"];
            //Do what you want with that value - such as storing it as you wanted.
            string connectionString = myPassedInValue;
            if (myPassedInValue.Contains("\\"))            
                connectionString = myPassedInValue.Remove(myPassedInValue.LastIndexOf("\\"), 1);   
            connectionString = connectionString.Replace("/","\\");
            string fileName = path.Trim() + "config.db";
            //string installationPath = Context.Parameters["assemblyPath"];

            using (FileStream fs = File.Create(fileName))
            {
                byte[] title = new UTF8Encoding(true).GetBytes(connectionString.Trim());
                fs.Write(title, 0, title.Length);
                InitializeComponent();
            }
        }
    }
}
