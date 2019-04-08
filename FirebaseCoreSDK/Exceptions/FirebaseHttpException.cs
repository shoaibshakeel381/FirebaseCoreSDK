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
            => Initialize(requestBody, responseBody, request, response);

        public FirebaseHttpException(
            string requestBody,
            string responseBody,
            HttpRequestMessage request,
            HttpResponseMessage response,
            Exception innerException)
            : this(innerException)
            => Initialize(requestBody, responseBody, request, response);

        public string RequestBody { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public string ResponseContent { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }

        private void Initialize(string requestBody, string responseBody, HttpRequestMessage request, HttpResponseMessage response)
        {
            RequestBody = requestBody;
            ResponseContent = responseBody;
            RequestMessage = request;
            ResponseMessage = response;
        }
    }
}