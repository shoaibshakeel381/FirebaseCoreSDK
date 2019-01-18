namespace FirebaseCoreSDK.Firebase.Database
{
    using System;

    using Configuration;

    using Extensions;

    using HttpClients.Database;

    public class DatabaseRef : IDatabaseRef
    {
        internal readonly IDatabaseHttpClient HttpClient;

        public string Path { get; set; }

        public DatabaseRef(IDatabaseHttpClient httpClient, FirebaseSDKConfiguration configuration, string refPath)
        {
            if (string.IsNullOrWhiteSpace(refPath))
            {
                throw new ArgumentNullException(nameof(refPath));
            }

            Path = $"{refPath.TrimSlashes()}.json";

            HttpClient = httpClient;
        }
    }
}
