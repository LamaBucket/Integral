namespace Integral.RestApi.Services
{
    public class FileLoggerService : ILogger
    {
        public static readonly string LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral/Log.txt");

        private static bool EnsureLogFileExists()
        {
            if (File.Exists(LogFilePath))
                return false;

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral"));

            File.Create(LogFilePath).Close();
            
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
                    EnsureLogFileExists();

                    string content = formatter.Invoke(state, exception);

                    File.AppendAllText(LogFilePath, content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.HelpLink);
            }
        }
    }
}
