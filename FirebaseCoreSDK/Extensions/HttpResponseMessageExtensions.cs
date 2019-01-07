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

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            response.Content?.Dispose();

            throw new FirebaseHttpException(content, response.RequestMessage, response);
        }
    }
}
