namespace FirebaseCoreSDK.Logging
{
    #region Namespace Imports

    using System;
    using System.Net.Http;

    #endregion


    public interface IFirebaseLogger
    {
        LogLevel LogLevel { get; set; }

        void Debug(string formatString, params object[] args);

        void Error(string formatString, params object[] args);

        void Info(string formatString, params object[] args);

        void OutgoingRequest(Uri url, HttpMethod method, string body, object response, int? resultCode, Exception e);

        void Warn(string formatString, params object[] args);
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