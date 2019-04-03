namespace FirebaseCoreSDK.Exceptions
{
    #region Namespace Imports

    using System;
    using System.Net.Http;

    #endregion


    public class FirebaseHttpException : FirebaseException
    {
        public FirebaseHttpException(string message)
            : base(message) {}

        public FirebaseHttpException(string message, Exception innerException)
            : base(message, innerException) {}

        public FirebaseHttpException(Exception innerException)
            : base(innerException.Message, innerException) {}

        public FirebaseHttpException(string requestBody, string responseBody, HttpRequestMessage request, HttpResponseMessage response)
        {
            RequestBody = requestBody;
            ResponseContent = responseBody;
            RequestMessage = request;
            ResponseMessage = response;
        }

        public string RequestBody { get; }
        public HttpRequestMessage RequestMessage { get; }
        public string ResponseContent { get; }
        public HttpResponseMessage ResponseMessage { get; }
    }
}