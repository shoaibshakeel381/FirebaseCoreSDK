namespace FirebaseCoreSDK.HttpClients.Database
{
    using System;

    using Configuration;

    using Extensions;

    using Firebase.Auth.ServiceAccounts;

    // ReSharper disable once RedundantNameQualifier
    using HttpClient = HttpClients.HttpClient;

    internal class DatabaseHttpClient : HttpClient, IDatabaseHttpClient
    {
        public DatabaseHttpClient(IServiceAccountCredentials credentials, FirebaseConfiguration configuration) 
            : base(credentials, configuration, new Uri($"{configuration.StorageBaseAuthority.TrimSlashes()}", UriKind.Absolute))
        {
            
        }

    }
}