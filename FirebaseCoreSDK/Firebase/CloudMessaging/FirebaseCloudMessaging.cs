namespace FirebaseCoreSDK.Firebase.CloudMessaging
{
    using System;
    using System.Threading.Tasks;

    using Auth.ServiceAccounts;

    using Configuration;

    using HttpClients.CloudMessaging;

    using Models;

    internal class FirebaseCloudMessaging : IFirebaseCloudMessaging
    {
        private readonly ICloudMessagingHttpClient _httpClient;

        public FirebaseCloudMessaging(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            _httpClient = new CloudMessagingHttpClient(credentials, configuration);
        }

        public async Task<PushMessageResponse> SendCloudMessageAsync(FirebasePushMessage request, bool dryRun = false)
        {
            request.Name = null;

            var message = new FirebasePushMessageEnvelope
            {
                DryRun = dryRun,
                Message = request
            };
            return await _httpClient.SendCloudMessageAsync(message).ConfigureAwait(false);
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