

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Text;

namespace AnalitikaAnketaDeltaMotors
{
    [RunInstaller(true)]

    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {

            this.Committed += new InstallEventHandler(MyInstaller_Committed);

        }
        public override void Commit(IDictionary savedState)
        {

            //string fileName = @"C:\New folder\text.txt";
            //string installationPath = Context.Parameters["assemblyPath"];

            //string connectionstring = "";

            //connectionstring = Context.Parameters["EDITA1"];
            //using (FileStream fs = File.Create(fileName))
            //{

            //    byte[] title = new UTF8Encoding(true).GetBytes(connectionstring);
            //    fs.Write(title, 0, title.Length);
            //    InitializeComponent();
            //}
        }
        private void MyInstaller_Committed(object sender, InstallEventArgs e)
        {

        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            
                base.Install(stateSaver);
                string myPassedInValue = this.Context.Parameters["conn".Trim()];
                string path = this.Context.Parameters["path"];
                //Do what you want with that value - such as storing it as you wanted.

                string fileName = path.Trim() + "config.db";
                //string installationPath = Context.Parameters["assemblyPath"];
                using (FileStream fs = File.Create(fileName))
                {

                    byte[] title = new UTF8Encoding(true).GetBytes(myPassedInValue.Trim());
                    fs.Write(title, 0, title.Length);
                    InitializeComponent();
                }
            
          
            
        }
    }
}



