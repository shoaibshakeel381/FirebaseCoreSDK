namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using System;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.HttpClients.Database;

    #endregion


    public class DatabaseRef : IDatabaseRef
    {
        internal readonly IDatabaseHttpClient HttpClient;

        public DatabaseRef(IDatabaseHttpClient httpClient, FirebaseSDKConfiguration configuration, string refPath)
        {
            if (string.IsNullOrWhiteSpace(refPath))
            {
                throw new ArgumentNullException(nameof(refPath));
            }

            Path = $"{refPath.TrimSlashes()}.json";

            HttpClient = httpClient;
        }

        public string Path { get; set; }
    }
}