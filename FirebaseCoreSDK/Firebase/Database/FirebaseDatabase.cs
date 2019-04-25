namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.HttpClients.Database;

    #endregion


    internal class FirebaseDatabase : IFirebaseDatabase
    {
        private readonly FirebaseSDKConfiguration _configuration;
        private readonly IDatabaseHttpClient _httpClient;

        public FirebaseDatabase(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new DatabaseHttpClient(credentials, configuration);
        }

        public IDatabaseRef Ref(string path) => new DatabaseRef(_httpClient, _configuration, path);
    }
}