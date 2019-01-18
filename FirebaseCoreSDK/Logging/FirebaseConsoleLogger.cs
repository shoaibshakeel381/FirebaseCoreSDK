namespace FirebaseCoreSDK.Logging
{
    using System;

    /// <summary>
    /// FirebaseConsoleLogger which logs to Console
    /// </summary>
    /// <seealso cref="FirebaseConsoleLogger" />
    public class FirebaseConsoleLogger : IFirebaseLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseConsoleLogger"/> class.
        /// </summary>
        public FirebaseConsoleLogger()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        /// <see cref="IFirebaseLogger.Debug"/>
        public void Debug(string formatString, params object[] args)
        {
            Console.WriteLine(Format("Debug", formatString, args));
        }

        /// <see cref="IFirebaseLogger.Info"/>
        public void Info(string formatString, params object[] args)
        {
            Console.WriteLine(Format("Info", formatString, args));
        }

        /// <see cref="IFirebaseLogger.Warn"/>
        public void Warn(string formatString, params object[] args)
        {
            Console.WriteLine(Format("Warn", formatString, args));
        }

        /// <see cref="IFirebaseLogger.Error"/>
        public void Error(string formatString, params object[] args)
        {
            Console.WriteLine(Format("Error", formatString, args));
        }

        private static string Format(string level, string formatString, params object[] args)
        {
            var message = args.Length > 0 ? string.Format(formatString, args) : formatString;

            return $"{DateTime.UtcNow} [FirebaseCoreSDK] [{level}] : {message}";
        }
    }
}
