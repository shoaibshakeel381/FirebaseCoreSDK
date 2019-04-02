using System;
using System.Net.Http;

namespace FirebaseCoreSDK.Logging
{
    /// <summary>
    /// FirebaseNullLogger which does not log.
    /// </summary>
    /// <seealso cref="FirebaseNullLogger" />
    public class FirebaseNullLogger : IFirebaseLogger
    {
        /// <see cref="IFirebaseLogger.Debug"/>
        public void Debug(string formatString, params object[] args)
        {
            // Log nothing
        }

        /// <see cref="IFirebaseLogger.Info"/>
        public void Info(string formatString, params object[] args)
        {
            // Log nothing
        }

        /// <see cref="IFirebaseLogger.Warn"/>
        public void Warn(string formatString, params object[] args)
        {
            // Log nothing
        }

        /// <see cref="IFirebaseLogger.Error"/>
        public void Error(string formatString, params object[] args)
        {
            // Log nothing
        }

        public void OutgoingRequest(Uri url, HttpMethod method, string body, object response, int? resultCode, Exception e)
        {
            // Log nothing
        }

        public LogLevel LogLevel { get; set; } = LogLevel.None;
    }
}
