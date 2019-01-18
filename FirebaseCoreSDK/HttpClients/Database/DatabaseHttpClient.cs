namespace FirebaseCoreSDK.HttpClients.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Configuration;

    using Firebase.Auth.ServiceAccounts;
    using Firebase.Database;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // ReSharper disable once RedundantNameQualifier
    using HttpClient = HttpClients.HttpClient;

    internal class DatabaseHttpClient : HttpClient, IDatabaseHttpClient
    {
        public DatabaseHttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration) 
            : base(credentials, configuration, new Uri(configuration.RealtimeDatabaseAuthority, UriKind.Absolute))
        {
            
        }

        public Task<T> GetFromPathAsync<T>(string path)
        {
            return GetFromPathAsync<T>(new Uri(path, UriKind.Relative));
        }

        public async Task<T> GetFromPathAsync<T>(Uri path)
        {
            var dataAsString = await SendAsync(() => PrepareGetRequest(path)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.DeserializeObject<T>(dataAsString, serializationOptions);
        }

        public Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(string path) where T : KeyEntity
        {
            return GetFromPathWithKeyInjectedAsync<T>(new Uri(path, UriKind.Relative));
        }

        public async Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(Uri path) where T : KeyEntity
        {
            var dataAsString = await SendAsync(() => PrepareGetRequest(path)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var result = JsonConvert.DeserializeObject<Dictionary<string, T>>(dataAsString, serializationOptions);

            return result.Select(s =>
            {
                if (s.Value != null)
                    s.Value.Key = s.Key;
                return s.Value;
            }).ToList();
        }

        public Task<T> SetToPathAsync<T>(string path, T content)
        {
            return SetToPathAsync(new Uri(path, UriKind.Relative), content);
        }

        public async Task<T> SetToPathAsync<T>(Uri path, T content)
        {
            var dataAsString = await SendAsync(() => PrepareSetRequest(path, content)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(dataAsString, serializationOptions);
        }

        public async Task<string> PushToPathAsync<T>(Uri path, T content)
        {
            var dataAsString = await SendAsync(() => PreparePushRequest(path, content)).ConfigureAwait(false);
            var firebaseKey = new { name = "" };
            var result = JsonConvert.DeserializeAnonymousType(dataAsString, firebaseKey);
            return result.name;
        }

        public Task<string> PushToPathAsync<T>(string path, T content)
        {
            return PushToPathAsync(new Uri(path, UriKind.Relative), content);
        }

        public Task<string> UpdatePathAsync(string path, IDictionary<string, object> content)
        {
            return UpdatePathAsync(new Uri(path, UriKind.Relative), content);
        }

        public async Task<string> UpdatePathAsync(Uri path, IDictionary<string, object> content)
        {
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            return await SendAsync(() => PreparePatchRequest(path, content)).ConfigureAwait(false);
        }

        public Task<T> UpdatePathAsync<T>(string path, IDictionary<string, object> content)
        {
            return UpdatePathAsync<T>(new Uri(path, UriKind.Relative), content);
        }

        public async Task<T> UpdatePathAsync<T>(Uri path, IDictionary<string, object> content)
        {
            var dataAsString = await SendAsync(() => PreparePatchRequest(path, content)).ConfigureAwait(false);

            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(dataAsString, serializationOptions);
        }

        public async Task DeletePathAsync(Uri path)
        {
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await SendAsync(() => PrepareDeleteRequest(path)).ConfigureAwait(false);
        }

        public Task DeletePathAsync(string path)
        {
            return DeletePathAsync(new Uri(path, UriKind.Relative));
        }

        private HttpRequestMessage PrepareGetRequest(Uri path)
        {
            var fullUri = GetFullAbsaluteUrl(path);
            var message = new HttpRequestMessage
            {
                RequestUri = fullUri,
                Method = HttpMethod.Get
            };
            AddAuthHeaders(message);
            return message;

        }

        private HttpRequestMessage PrepareSetRequest<T>(Uri path, T content)
        {
            return PrepareContentRequest(path, content, HttpMethod.Put);
        }

        private HttpRequestMessage PreparePushRequest<T>(Uri path, T content)
        {
            return PrepareContentRequest(path, content, HttpMethod.Post);
        }

        private HttpRequestMessage PreparePatchRequest(Uri path, IDictionary<string, object> content)
        {
            return PrepareContentRequest(path, content, HttpMethod.Patch);
        }

        private HttpRequestMessage PrepareContentRequest<T>(Uri path, T content, HttpMethod method)
        {
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var stringContent = JsonConvert.SerializeObject(content, serializationOptions);
            var jsonContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            var fullUri = GetFullAbsaluteUrl(path);

            var message = new HttpRequestMessage
            {
                RequestUri = fullUri,
                Method = method,
                Content = jsonContent
            };

            AddAuthHeaders(message);
            return message;
        }

        private HttpRequestMessage PrepareDeleteRequest(Uri path)
        {
            var fullUri = GetFullAbsaluteUrl(path);
            var message = new HttpRequestMessage
            {
                RequestUri = fullUri,
                Method = HttpMethod.Delete
            };
            AddAuthHeaders(message);
            return message;

        }
    }
}