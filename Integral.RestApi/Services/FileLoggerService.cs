namespace Integral.RestApi.Services
{
    public class FileLoggerService : ILogger
    {
        public static readonly string ErrorLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral/ErrorLog.txt");
        public static readonly string InformationLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral/InformationLog.txt");

        private static bool EnsureLogFileExists(string path)
        {
            if (File.Exists(path))
                return false;

            string? dir = Path.GetDirectoryName(path);

            if (dir is not null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.Create(path);
            
            return true;
        }


        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                if (formatter != null)
                {
                    string path = InformationLogFilePath;

                    switch (logLevel)
                    {
                        case LogLevel.Information:
                            path = InformationLogFilePath;
                            break;
                        case >= LogLevel.Error:
                            path = ErrorLogFilePath;
                            break;

                    }

                    EnsureLogFileExists(path);


                    string content = formatter.Invoke(state, exception);

                    File.AppendAllText(path, content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.HelpLink);
            }
        }
    }
}
