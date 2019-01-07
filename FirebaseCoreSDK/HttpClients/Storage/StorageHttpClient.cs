namespace FirebaseCoreSDK.HttpClients.Storage
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Configuration;

    using Extensions;

    using Firebase.Auth.ServiceAccounts;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // ReSharper disable once RedundantNameQualifier
    using HttpClient = HttpClients.HttpClient;

    internal class StorageHttpClient : HttpClient, IStorageHttpClient
    {
        public StorageHttpClient(IServiceAccountCredentials credentials, FirebaseConfiguration configuration) 
            : base(credentials, configuration, new Uri($"{configuration.StorageBaseAuthority.TrimSlashes()}", UriKind.Absolute))
        {
            
        }

        public async Task<T> SendStorageRequestAsync<T>(Uri path, HttpMethod method)
        {
            var dataAsString = await SendAsync(() => PrepareStorageRequest(path, method)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(dataAsString, serializationOptions);
        }

        private HttpRequestMessage PrepareStorageRequest(Uri path, HttpMethod method)
        {
            var fullUri = GetFullAbsaluteUrl(path);

            var message = new HttpRequestMessage
            {
                RequestUri = fullUri,
                Method = method
            };
            AddAuthHeaders(message);
            return message;
        }
    }
}