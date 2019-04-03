namespace FirebaseCoreSDK.Logging
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Text;

    #endregion


    /// <summary>
    ///     FirebaseConsoleLogger which logs to Console
    /// </summary>
    /// <seealso cref="FirebaseConsoleLogger" />
    public class FirebaseConsoleLogger : IFirebaseLogger
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FirebaseConsoleLogger" /> class.
        /// </summary>
        public FirebaseConsoleLogger() => Console.OutputEncoding = Encoding.UTF8;

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        /// <see cref="IFirebaseLogger.Debug" />
        public void Debug(string formatString, params object[] args) => Console.WriteLine(Format("Debug", formatString, args));

        /// <see cref="IFirebaseLogger.Error" />
        public void Error(string formatString, params object[] args) => Console.WriteLine(Format("Error", formatString, args));

        /// <see cref="IFirebaseLogger.Info" />
        public void Info(string formatString, params object[] args) => Console.WriteLine(Format("Info", formatString, args));

        public void OutgoingRequest(Uri url, HttpMethod method, string body, object response, int? resultCode, Exception e)
        {
            // Log nothing
        }

        /// <see cref="IFirebaseLogger.Warn" />
        public void Warn(string formatString, params object[] args) => Console.WriteLine(Format("Warn", formatString, args));

        private static string Format(string level, string formatString, params object[] args)
        {
            var message = args.Length > 0 ? string.Format(formatString, args) : formatString;

            return $"{DateTime.UtcNow} [FirebaseCoreSDK] [{level}] : {message}";
        }
    }
}