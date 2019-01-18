namespace FirebaseCoreSDK.Logging
{
    public interface IFirebaseLogger
    {
        LogLevel LogLevel { get; set; }

        void Debug(string formatString, params object[] args);

        void Info(string formatString, params object[] args);

        void Warn(string formatString, params object[] args);

        void Error(string formatString, params object[] args);
    }

    public enum LogLevel
    {
        None = 0,
        Debug,
        Info,
        Warning,
        Error
    }
}
