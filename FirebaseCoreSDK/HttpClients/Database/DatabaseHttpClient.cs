namespace FirebaseCoreSDK.HttpClients.Database
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.Firebase.Database;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // ReSharper disable once RedundantNameQualifier

    #endregion


    internal class DatabaseHttpClient : FirebaseHttpClient, IDatabaseHttpClient
    {
        public DatabaseHttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
            : base(credentials, configuration, new Uri(configuration.RealtimeDatabaseAuthority, UriKind.Absolute)) {}

        public async Task DeletePathAsync(Uri path) => await SendAsync(() => PrepareDeleteRequest(path)).ConfigureAwait(false);

        public Task DeletePathAsync(string path) => DeletePathAsync(new Uri(path, UriKind.Relative));

        public Task<T> GetFromPathAsync<T>(string path) => GetFromPathAsync<T>(new Uri(path, UriKind.Relative));

        public async Task<T> GetFromPathAsync<T>(Uri path)
        {
            var dataAsString = await SendAsync(() => PrepareGetRequest(path)).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(dataAsString, Client.SerializerSettings);
        }

        public Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(string path) where T : IKeyEntity
            => GetFromPathWithKeyInjectedAsync<T>(new Uri(path, UriKind.Relative));

        public async Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(Uri path) where T : IKeyEntity
        {
            var dataAsString = await SendAsync(() => PrepareGetRequest(path)).ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<Dictionary<string, T>>(dataAsString, Client.SerializerSettings);

            return result?.Select(
                    s =>
                    {
                        if (s.Value != null)
                        {
                            s.Value.Key = s.Key;
                        }

                        return s.Value;
                    })
                .ToList();
        }

        public async Task<string> PushToPathAsync<T>(Uri path, T content)
        {
            var dataAsString = await SendAsync(() => PreparePushRequest(path, content)).ConfigureAwait(false);
            var firebaseKey = new { name = "" };
            var result = JsonConvert.DeserializeAnonymousType(dataAsString, firebaseKey);
            return result.name;
        }

        public Task<string> PushToPathAsync<T>(string path, T content) => PushToPathAsync(new Uri(path, UriKind.Relative), content);

        public Task<T> SetToPathAsync<T>(string path, T content) => SetToPathAsync(new Uri(path, UriKind.Relative), content);

        public async Task<T> SetToPathAsync<T>(Uri path, T content)
        {
            var dataAsString = await SendAsync(() => PrepareSetRequest(path, content)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(dataAsString, Client.SerializerSettings);
        }

        public Task<string> UpdatePathAsync(string path, IDictionary<string, object> content) => UpdatePathAsync(new Uri(path, UriKind.Relative), content);

        public async Task<string> UpdatePathAsync(Uri path, IDictionary<string, object> content)
            => await SendAsync(() => PreparePatchRequest(path, content)).ConfigureAwait(false);

        public Task<T> UpdatePathAsync<T>(string path, IDictionary<string, object> content) => UpdatePathAsync<T>(new Uri(path, UriKind.Relative), content);

        public async Task<T> UpdatePathAsync<T>(Uri path, IDictionary<string, object> content)
        {
            var dataAsString = await SendAsync(() => PreparePatchRequest(path, content)).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(dataAsString, Client.SerializerSettings);
        }

        public async Task<T> UpdatePathWithKeyInjectedAsync<T>(string path, IEnumerable<T> contentList) where T : IKeyEntity
        {
            var dataAsString = await SendAsync(() => PrepareContentRequestWithKey(new Uri(path, UriKind.Relative), contentList, HttpMethod.Patch)).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(dataAsString, Client.SerializerSettings);
        }

        private HttpRequestMessage PrepareContentRequest<T>(Uri path, T content, HttpMethod method) 
        {
            var stringContent = JsonConvert.SerializeObject(content, Client.SerializerSettings);
            var jsonContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            var fullUri = GetFullAbsoluteUrl(path);

            var message = new HttpRequestMessage { RequestUri = fullUri, Method = method, Content = jsonContent };

            AddAuthHeaders(message);
            return message;
        }

        private HttpRequestMessage PrepareContentRequestWithKey<T>(Uri path, IEnumerable<T> content, HttpMethod method) where T : IKeyEntity
        {
            var dictionary = content.ToDictionary(x => x.Key, x=> x);

            var stringContent = JsonConvert.SerializeObject(dictionary, Client.SerializerSettings);
            var jsonContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            var fullUri = GetFullAbsoluteUrl(path);

            var message = new HttpRequestMessage { RequestUri = fullUri, Method = method, Content = jsonContent };

            AddAuthHeaders(message);
            return message;
        }

        private HttpRequestMessage PrepareDeleteRequest(Uri path)
        {
            var fullUri = GetFullAbsoluteUrl(path);
            var message = new HttpRequestMessage { RequestUri = fullUri, Method = HttpMethod.Delete };
            AddAuthHeaders(message);
            return message;
        }

        private HttpRequestMessage PrepareGetRequest(Uri path)
        {
            var fullUri = GetFullAbsoluteUrl(path);
            var message = new HttpRequestMessage { RequestUri = fullUri, Method = HttpMethod.Get };
            AddAuthHeaders(message);
            return message;
        }

        private HttpRequestMessage PreparePatchRequest(Uri path, IDictionary<string, object> content) => PrepareContentRequest(path, content, HttpMethod.Patch);

        private HttpRequestMessage PreparePushRequest<T>(Uri path, T content) => PrepareContentRequest(path, content, HttpMethod.Post);

        private HttpRequestMessage PrepareSetRequest<T>(Uri path, T content) => PrepareContentRequest(path, content, HttpMethod.Put);
    }
}