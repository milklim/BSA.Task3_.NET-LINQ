using System;

namespace LogInfo
{
    public class LogToConsole : OuterSource
    {
        public override void Write(string message)
        {
                System.Console.WriteLine(message);
        }
    }
}
