namespace FirebaseCoreSDK.Logging
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Text;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     FirebaseConsoleLogger which logs to Console
    /// </summary>
    /// <seealso cref="FirebaseNullLogger" />
    public class FirebaseConsoleLogger : IFirebaseLogger
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FirebaseConsoleLogger" /> class.
        /// </summary>
        public FirebaseConsoleLogger()
            : this(LogLevel.Debug)
            => Console.OutputEncoding = Encoding.UTF8;

        public FirebaseConsoleLogger(LogLevel logLevel) => LogLevel = logLevel;

        /// <inheritdoc />
        public LogLevel LogLevel { get; }

        /// <inheritdoc />
        public virtual void Debug(string formatString, params object[] args)
        {
            if ((LogLevel & LogLevel.Debug) == LogLevel.Debug)
            {
                Console.WriteLine(Format("Debug", formatString, args));
            }
        }

        /// <inheritdoc />
        public virtual void Error(string formatString, params object[] args)
        {
            if ((LogLevel & LogLevel.Error) == LogLevel.Error)
            {
                Console.WriteLine(Format("Error", formatString, args));
            }
        }

        /// <inheritdoc />
        public virtual void Info(string formatString, params object[] args)
        {
            if ((LogLevel & LogLevel.Info) == LogLevel.Info)
            {
                Console.WriteLine(Format("Info", formatString, args));
            }
        }

        /// <inheritdoc />
        public virtual void OutgoingRequestCompleted(object response, int? resultCode, Exception e)
        {
            if ((LogLevel & LogLevel.Debug) == LogLevel.Debug)
            {
                Console.WriteLine(Format("RQOUTRP", JsonConvert.SerializeObject(response), null));
            }
        }

        public virtual void OutgoingRequestInitiated(Uri url, HttpMethod method, string body)
        {
            if ((LogLevel & LogLevel.Debug) == LogLevel.Debug)
            {
                Console.WriteLine(Format("RQOUTRQ", $"Request sent to URL=[{method.Method}][{url}] with body={JsonConvert.SerializeObject(body)}", null));
            }
        }

        /// <inheritdoc />
        public virtual void Warn(string formatString, params object[] args)
        {
            if ((LogLevel & LogLevel.Warning) == LogLevel.Warning)
            {
                Console.WriteLine(Format("Warn", formatString, args));
            }
        }

        private static string Format(string level, string formatString, params object[] args)
        {
            var message = args != null && args.Length > 0 ? string.Format(formatString, args) : formatString;

            return $"{DateTime.UtcNow} [FirebaseCoreSDK] [{level}] : {message}";
        }
    }
}