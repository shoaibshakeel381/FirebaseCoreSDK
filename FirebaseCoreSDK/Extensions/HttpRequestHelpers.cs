namespace FirebaseCoreSDK.Extensions
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Exceptions;
    using FirebaseCoreSDK.Logging;

    #endregion


    public static class HttpRequestHelpers
    {
        public static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, HttpRequestMessage request, Exception ex)
        {
            if (ex != null)
            {
                throw new FirebaseHttpException(await GetRequestContent(request), null, request, null, ex);
            }

            if (response == null)
            {
                throw new FirebaseHttpException(await GetRequestContent(request), null, request, null);
            }

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var requestContent = await GetRequestContent(request);
            var content = await GetResponseContent(response);

            response.Content?.Dispose();
            response.RequestMessage.Content?.Dispose();

            throw new FirebaseHttpException(requestContent, content, response.RequestMessage, response);
        }

        public static async Task LogOutgoingRequestCompleted(HttpResponseMessage response, IFirebaseLogger logger, Exception ex)
            => logger?.OutgoingRequestCompleted(await GetResponseContent(response), (int?)response.StatusCode, ex);

        public static async Task LogOutgoingRequestInitiated(HttpRequestMessage request, IFirebaseLogger logger)
            => logger?.OutgoingRequestInitiated(request.RequestUri, request.Method, await GetRequestContent(request));

        private static async Task<string> GetRequestContent(HttpRequestMessage request)
        {
            if (null != request.Content)
            {
                return await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return null;
        }

        private static async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            string content = null;

            if (response.Content != null)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return content;
        }
    }
}