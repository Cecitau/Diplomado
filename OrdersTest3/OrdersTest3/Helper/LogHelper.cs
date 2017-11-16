using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Helper
{
    public static class LogHelper
    {
        public static async void WriteLog(String Text)
        {
            using (StreamWriter writer = new StreamWriter("LogFile.txt", true))
            {
                DateTime LogDateTime = DateTime.Now;
                await writer.WriteLineAsync(string.Format("Log - {0} : {1}", LogDateTime, Text));

            }
        }


        
    }
}
