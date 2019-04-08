namespace FirebaseCoreSDK.Logging
{
    #region Namespace Imports

    using System;
    using System.Net.Http;

    #endregion


    /// <summary>
    ///     Logs activities performed in the nuget package. Most interesting logs are
    /// </summary>
    public interface IFirebaseLogger
    {
        LogLevel LogLevel { get; }

        void Debug(string formatString, params object[] args);

        void Error(string formatString, params object[] args);

        void Info(string formatString, params object[] args);

        void OutgoingRequestCompleted(object response, int? resultCode, Exception e);

        void OutgoingRequestInitiated(Uri url, HttpMethod method, string body);

        void Warn(string formatString, params object[] args);
    }
}