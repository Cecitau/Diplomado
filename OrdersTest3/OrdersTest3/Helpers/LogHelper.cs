using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Helpers
{
    public static class LogHelper
    {
        public static async void WriteLog(string Text)
        {
            using (StreamWriter writer = new StreamWriter("LogFile.txt", true))
            {
                DateTime logDateTime = DateTime.Now;
                await writer.WriteLineAsync(string.Format("Log - {0} : {1}", logDateTime, Text));
                
            }
        }
    }
}
