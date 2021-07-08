using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalitikaAnketaDeltaMotors.Classes
{
    public class Configuration
    {
        public string ConnectionString { get; set; }

        private static Configuration _instance = null;

        public static Configuration GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Configuration();
                _instance.GetConnectionString();
            }
            return _instance;
        }

        public void GetConnectionString()
        {
            ConnectionString = ConfigHelper.ReadConfigFile();
        }

        public void ResetConnectionString()
        {
            ConnectionString = ConfigHelper.ReadConfigFile();
        }
    }
}
