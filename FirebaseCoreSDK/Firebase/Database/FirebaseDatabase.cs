namespace FirebaseCoreSDK.Firebase.Database
{
    using System;

    using Auth.ServiceAccounts;

    using Configuration;

    using HttpClients.Database;

    internal class FirebaseDatabase : IFirebaseDatabase
    {
        private readonly FirebaseConfiguration _configuration;
        private readonly IDatabaseHttpClient _httpClient;

        public FirebaseDatabase(IServiceAccountCredentials credentials, FirebaseConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new DatabaseHttpClient(credentials, configuration);
        }

        public IDatabaseRef Ref(string path)
        {
            return new DatabaseRef(_httpClient, _configuration, path);
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

        ~FirebaseDatabase()
        {
            Dispose(false);
        }
        #endregion
    }
}