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

            string request = null;
            string content = null;
            if(null != response.RequestMessage.Content)
                request = await response.RequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.Content != null)
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            response.Content?.Dispose();
            response.RequestMessage.Content?.Dispose();

            throw new FirebaseHttpException(request, content, response.RequestMessage, response);
        }
    }
}
