namespace FirebaseCoreSDK.Firebase.Auth
{
    #region Namespace Imports

    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.HttpClients.Auth;

    #endregion


    internal class FirebaseAuth : IFirebaseAuth
    {
        private readonly FirebaseSDKConfiguration _configuration;
        private readonly IAuthHttpClient _httpClient;

        public FirebaseAuth(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new AuthHttpClient(credentials, configuration);
        }

        public async Task AuthenticateAsync()
        {
            var result = await _httpClient.AuthenticateAsync().ConfigureAwait(false);
            _configuration.AccessToken = result;
        }

        public string CreateCustomToken(string userId) => _httpClient.CreateCustomToken(userId);
    }
}