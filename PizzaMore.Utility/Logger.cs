using System;
using System.IO;

namespace PizzaMore.Utility
{
    public static class Logger
    {
        //static method that takes a string message and appends it as a text on a new line to a log.txt file.
        public static void Log(string message)
        {
            File.AppendAllText(@"Logs/log.txt", message + Environment.NewLine);
        }
    }
}
