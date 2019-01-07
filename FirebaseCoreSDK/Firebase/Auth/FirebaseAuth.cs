namespace FirebaseCoreSDK.Firebase.Auth
{
    using System;
    using System.Threading.Tasks;

    using Configuration;

    using HttpClients.Auth;

    using ServiceAccounts;

    internal class FirebaseAuth : IFirebaseAuth
    {
        private readonly FirebaseConfiguration _configuration;
        private readonly IAuthHttpClient _httpClient;

        public FirebaseAuth(IServiceAccountCredentials credentials, FirebaseConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new AuthHttpClient(credentials, configuration);
        }

        public string CreateCustomToken(long userId)
        {
            return _httpClient.CreateCustomToken(userId);
        }

        public async Task AuthenticateAsync()
        {
            var result = await _httpClient.AuthenticateAsync().ConfigureAwait(false);
            _configuration.AccessToken = result;
        }

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

        ~FirebaseAuth()
        {
            Dispose(false);
        }
    }
}