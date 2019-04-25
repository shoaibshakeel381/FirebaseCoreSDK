namespace FirebaseCoreSDK.HttpClients
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Exceptions;

    #endregion


    public class TransientHttpClientProxy : IHttpClientProxy
    {
        private readonly FirebaseSDKConfiguration _configuration;

        public TransientHttpClientProxy() {}

        public TransientHttpClientProxy(FirebaseSDKConfiguration configuration) => _configuration = configuration;

        /// <inheritdoc />
        public virtual async Task<string> SendAsync(Func<HttpRequestMessage> requestMessage)
        {
            using (var client = new HttpClient())
            {
                var request = requestMessage();

                HttpResponseMessage response = null;
                Exception ex = null;

                try
                {
                    await LogOutgoingRequestInitiated(request);
                    response = await client.SendAsync(request).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    ex = exception;
                }

                await LogOutgoingRequestCompleted(response, ex);
                await EnsureSuccessStatusCodeAsync(response, request, ex).ConfigureAwait(false);

                return await GetResponseContent(response);
            }
        }

        protected static async Task<string> GetRequestContent(HttpRequestMessage request)
        {
            if (null != request.Content)
            {
                return await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return null;
        }

        protected static async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            string content = null;

            if (response?.Content != null)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return content;
        }

        protected static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, HttpRequestMessage request, Exception ex)
        {
            var requestContent = await GetRequestContent(request);
            var content = await GetResponseContent(response);

            if (ex != null)
            {
                throw new FirebaseHttpException(requestContent, content, request, response, ex);
            }

            if (response == null)
            {
                throw new FirebaseHttpException(requestContent, null, request, null);
            }

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            response.Content?.Dispose();
            response.RequestMessage.Content?.Dispose();

            throw new FirebaseHttpException(requestContent, content, response.RequestMessage, response);
        }

        private async Task LogOutgoingRequestCompleted(HttpResponseMessage response, Exception ex)
            => _configuration.Logger?.OutgoingRequestCompleted(await GetResponseContent(response), (int?)response?.StatusCode, ex);

        private async Task LogOutgoingRequestInitiated(HttpRequestMessage request)
            => _configuration.Logger?.OutgoingRequestInitiated(request.RequestUri, request.Method, await GetRequestContent(request));
    }
}