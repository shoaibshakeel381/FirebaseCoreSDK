namespace FirebaseCoreSDK.Exceptions
{
    using System;
    using System.Net.Http;

    public class FirebaseHttpException : Exception
    {
        public HttpRequestMessage RequestMessage { get; }
        public HttpResponseMessage ResponseMessage { get; }
        public string RequestBody { get; }
        public string ResponseContent { get; }

        public FirebaseHttpException(string message)
            : base(message)
        {
        }
        public FirebaseHttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public FirebaseHttpException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
        public FirebaseHttpException(string requestBody, string responseBody,
            HttpRequestMessage request, HttpResponseMessage response)
        {
            RequestBody = requestBody;
            ResponseContent = responseBody;
            RequestMessage = request;
            ResponseMessage = response;
        }
    }
}
