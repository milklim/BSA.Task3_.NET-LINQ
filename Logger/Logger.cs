using System;

namespace LogInfo
{
    public class Logger
    {
  #region Singleton - there is only one instance of this class
        private Logger() { Source = new LogToConsole(); }  // default: console output
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger()); 
        public static Logger Instance { get { return instance.Value; } }
  #endregion

        public static OuterSource Source { get; set; }

        public void Info(string message)
        {
            Source.Write(string.Format(">>> [info] {0}", message));
        }
        public void Debug(string message)
        {
            Source.Write(string.Format(">>> [debug] {0}", message));
        }
        public void Warning(string message)
        {
            Source.Write(string.Format(">>> [warnings] {0}", message));
        }
        public void Error(string message)
        {
            Source.Write(string.Format(">>> [ERROR] {0}", message));
        }

    }
}
