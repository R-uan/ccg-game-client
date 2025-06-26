using System.Runtime.CompilerServices;

namespace GameClient
{
    public static class Logger
    {
        public enum LogLevel { Info, Warning, Error, Debug }

        public static void Debug(string message, [CallerMemberName] string caller = "") => Write(message, LogLevel.Info, caller);
        public static void Info(string message, [CallerMemberName] string caller = "") => Write(message, LogLevel.Info, caller);
        public static void Warn(string message, [CallerMemberName] string caller = "") => Write(message, LogLevel.Warning, caller);
        public static void Error(string message, [CallerMemberName] string caller = "") => Write(message, LogLevel.Error, caller);

        private static void Write(string message, LogLevel level, string caller)
        {
            string prefix = level switch
            {
                LogLevel.Info => "[INFO]",
                LogLevel.Warning => "[WARN]",
                LogLevel.Error => "[ERROR]",
                LogLevel.Debug => "[DEBUG]",
                _ => "[LOG]"
            };

            string output = $"{prefix} [{DateTime.Now.Date:dd/MM/yyyy} {DateTime.Now:HH:mm:ss}] [{caller}] {message}";
            System.Console.WriteLine(output);
        }
    }
}