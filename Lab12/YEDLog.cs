using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace lab12 {
    public enum YEDLogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
    }

    public class YEDLogException : Exception {
        public YEDLogException(string message) : base(message) { }
    }

    public class YEDLog {
        private record YEDLogRecord(
            string Year,
            string Month,
            string Day,
            string Hour,
            string Minute,
            string Second,
            string Level,
            string Path,
            string Caller,
            string Line,
            string Message
        );

        private readonly string _logFilePath;
        private readonly string _logFormat =
            "[%Day%.%Month%.%Year% %Hour%:%Minute%:%Second%][%Level%][%Path%:%Caller%:%Line%]: %Message%"
        ;

        public string LogFilePath {
            get => _logFilePath;
        }

        public string LogFormat {
            get => _logFormat;
        }

        public YEDLog(string logFilePath) {
            string? logFileDirectory = Path.GetDirectoryName(logFilePath) ?? Directory.GetCurrentDirectory();
            string? logFileName = Path.GetFileName(logFilePath);

            if (string.IsNullOrWhiteSpace(logFileName)) {
                throw new YEDLogException("Log file name is not specified");
            }

            _logFilePath = Path.Combine(logFileDirectory, logFileName);

            if (!Directory.Exists(logFileDirectory)) {
                Directory.CreateDirectory(logFileDirectory);
            }
            if (!File.Exists(_logFilePath)) {
                WriteToFile(_logFilePath, "", false);
            }

        }

        public YEDLog(string logFilePath, string logFormat)
            : this(logFilePath) {
            _logFormat = logFormat;
        }

        public void Log(
            YEDLogLevel logLevel,
            string message,
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        ) {
            DateTime datetime = DateTime.Now;
            string formattedLog = FormatMessage(
                _logFormat,
                new YEDLogRecord(
                    datetime.Year.ToString("D4"),
                    datetime.Month.ToString("D2"),
                    datetime.Day.ToString("D2"),
                    datetime.Hour.ToString("D2"),
                    datetime.Minute.ToString("D2"),
                    datetime.Second.ToString("D2"),
                    logLevel.ToString().ToUpper(),
                    callerFilePath,
                    callerName,
                    callerLineNumber.ToString(),
                    message
                )
            );
            WriteToFile(_logFilePath, formattedLog);
        }

        public void Debug(
            string message,
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        ) => Log(YEDLogLevel.Debug, message, callerName, callerFilePath, callerLineNumber);

        public void Info(
            string message,
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        ) => Log(YEDLogLevel.Info, message, callerName, callerFilePath, callerLineNumber);

        public void Warning(
            string message,
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        ) => Log(YEDLogLevel.Warning, message, callerName, callerFilePath, callerLineNumber);

        public void Error(
            string message,
            [System.Runtime.CompilerServices.CallerMemberName] string callerName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        ) => Log(YEDLogLevel.Error, message, callerName, callerFilePath, callerLineNumber);


        public string GetLogs() {
            return ReadFromFile(_logFilePath);
        }

        public string FindLogs(string substring, bool ignoreCase = false) {
            var logs = ReadFromFile(_logFilePath).Split(Environment.NewLine);
            var selectedLogs = ignoreCase
                                    ? logs.Where(log => log.ToLower().Contains(substring.ToLower()))
                                    : logs.Where(log => log.Contains(substring))
            ;
            return string.Join(Environment.NewLine, selectedLogs);
        }

        public void ClearLogs() {
            ClearFile(_logFilePath);
        }

        private static string FormatMessage(string logFormat, YEDLogRecord record) {
            var fields = record.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string message = logFormat;
            foreach (var field in fields) {
                message = message.Replace($"%{field.Name}%", field.GetValue(record)?.ToString());
            }
            return message;
        }

        private static void WriteToFile(string filePath, string text, bool withNewLine = true) {
            try {
                File.AppendAllText(
                    filePath,
                    text + (withNewLine ? Environment.NewLine : "")
                );
            }
            catch (Exception ex) {
                throw new YEDLogException($"Can't write to file '{filePath}': {ex.Message}");
            }
        }

        private static string ReadFromFile(string filePath) {
            try {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex) {
                throw new YEDLogException($"Can't read from file '{filePath}': {ex.Message}");
            }
        }

        private static void ClearFile(string filePath) {
            try {
                File.WriteAllText(filePath, "");
            }
            catch (Exception ex) {
                throw new YEDLogException($"Can't clear file '{filePath}': {ex.Message}");
            }
        }
    }
}
