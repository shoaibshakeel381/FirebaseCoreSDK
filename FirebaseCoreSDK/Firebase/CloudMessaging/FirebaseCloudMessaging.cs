namespace FirebaseCoreSDK.Firebase.CloudMessaging
{
    using System;
    using System.Threading.Tasks;

    using Auth.ServiceAccounts;

    using Configuration;

    using HttpClients.CloudMessagign;

    using Models;

    internal class FirebaseCloudMessaging : IFirebaseCloudMessaging
    {
        private readonly ICloudMessagingHttpClient _httpClient;

        public FirebaseCloudMessaging(IServiceAccountCredentials credentials, FirebaseConfiguration configuration)
        {
            _httpClient = new CloudMessagingHttpClient(credentials, configuration);
        }

        public async Task<FirebasePushEnvelopeMessage> SendCloudMessageAsync(FirebasePushEnvelope request)
        {
            request.Message.Name = null;

            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            return await _httpClient.SendCloudMessageAsync(request).ConfigureAwait(false);
        }

        #region Dispose Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _httpClient?.Dispose();

        }

        ~FirebaseCloudMessaging()
        {
            Dispose(false);
        }
        #endregion
    }
}