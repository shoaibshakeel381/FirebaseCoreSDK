namespace FirebaseCoreSDK.Logging
{
    /// <summary>
    ///     FirebaseNullLogger which does not log.
    /// </summary>
    /// <seealso cref="FirebaseConsoleLogger" />
    public class FirebaseNullLogger : FirebaseConsoleLogger
    {
        public FirebaseNullLogger()
            : this(LogLevel.None) {}

        public FirebaseNullLogger(LogLevel logLevel)
            : base(logLevel) {}
    }
}