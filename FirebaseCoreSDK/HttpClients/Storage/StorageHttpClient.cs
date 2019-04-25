namespace FirebaseCoreSDK.HttpClients.Storage
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // ReSharper disable once RedundantNameQualifier

    #endregion


    internal class StorageHttpClient : FirebaseHttpClient, IStorageHttpClient
    {
        public StorageHttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
            : base(credentials, configuration, new Uri($"{configuration.StorageBaseAuthority.TrimSlashes()}", UriKind.Absolute)) {}

        public async Task<T> SendStorageRequestAsync<T>(Uri path, HttpMethod method)
        {
            var dataAsString = await SendAsync(() => PrepareStorageRequest(path, method)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.DeserializeObject<T>(dataAsString, serializationOptions);
        }

        private HttpRequestMessage PrepareStorageRequest(Uri path, HttpMethod method)
        {
            var fullUri = GetFullAbsoluteUrl(path);

            var message = new HttpRequestMessage { RequestUri = fullUri, Method = method };
            AddAuthHeaders(message);
            return message;
        }
    }
}