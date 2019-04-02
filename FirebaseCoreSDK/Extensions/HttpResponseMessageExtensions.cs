using FirebaseCoreSDK.Logging;

namespace FirebaseCoreSDK.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Exceptions;

    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var request = await GetRequestContent(response);

            var content = await GetResponseContent(response);

            response.Content?.Dispose();
            response.RequestMessage.Content?.Dispose();

            throw new FirebaseHttpException(request, content, response.RequestMessage, response);
        }

        private static async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            string content = null;
            if (response.Content != null)
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        private static async Task<string> GetRequestContent(HttpResponseMessage response)
        {
            string request = null;
            if (null != response.RequestMessage.Content)
                request = await response.RequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return request;
        }

        public static async Task  LogRequest(this HttpResponseMessage response, IFirebaseLogger logger)
        {
            if (logger == null)
                return;

            var request = response.RequestMessage;
            logger.OutgoingRequest(request.RequestUri, request.Method, await GetRequestContent(response), await GetResponseContent(response), (int?) response.StatusCode, null);
        }
    }
}
