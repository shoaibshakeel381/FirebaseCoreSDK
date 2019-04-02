using System;
using System.Net.Http;

namespace FirebaseCoreSDK.Logging
{
    public interface IFirebaseLogger
    {
        LogLevel LogLevel { get; set; }

        void Debug(string formatString, params object[] args);

        void Info(string formatString, params object[] args);

        void Warn(string formatString, params object[] args);

        void Error(string formatString, params object[] args);

        void OutgoingRequest(Uri url, HttpMethod method, string body, object response, int? resultCode, Exception e);
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
