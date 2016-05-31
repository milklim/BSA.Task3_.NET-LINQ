using System;
using System.IO;

namespace LogInfo
{
    public class LogToFile : OuterSource
    {
        public LogToFile()
        {
            if (File.Exists(pathToLog))
            {
                if (new FileInfo(pathToLog).Length > 500000)
                    File.Move(pathToLog, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Log_{0:dd.MM.yyy}.log", DateTime.Now)));
            }
        }

        private static string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CurrentLog.log");
        
        public override void Write(string message)
        {
                using (StreamWriter log = new StreamWriter(pathToLog, true))
                {
                    log.WriteLine(message);
                }
        }
    }
}
