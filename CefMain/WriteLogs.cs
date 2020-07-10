using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefMain
{
    public class WriteLogs
    {
        //记录系统日志
        public void WriteLog(string StrLog)
        {

            using (System.IO.FileStream stream = new System.IO.FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Log" + "_" + DateTime.Today.ToLongDateString() + ".log", System.IO.FileMode.Append))
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
            {
                writer.WriteLine(StrLog);
                writer.Close();
                writer.Dispose();
                stream.Close();
                stream.Dispose();
                GC.Collect();
            }

            StrLog = null;
            GC.Collect();
        }
    }

}
