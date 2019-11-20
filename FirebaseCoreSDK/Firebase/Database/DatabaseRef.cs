namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using System;
    using System.Web;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.HttpClients.Database;

    #endregion


    public class DatabaseRef : IDatabaseRef
    {
        internal readonly IDatabaseHttpClient HttpClient;

        public DatabaseRef(IDatabaseHttpClient httpClient, FirebaseSDKConfiguration configuration, string refPath, QueryBuilder queryBuilder = null )
        {
            if (string.IsNullOrWhiteSpace(refPath))
            {
                throw new ArgumentNullException(nameof(refPath));
            }

            Path = queryBuilder != null ? $"{refPath.TrimSlashes()}.json?{queryBuilder.ToQueryString()}" : $"{refPath.TrimSlashes()}.json";

            HttpClient = httpClient;
        }

        public string Path { get; set; }
    }
}