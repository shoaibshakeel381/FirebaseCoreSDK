namespace FirebaseCoreSDK.HttpClients.CloudMessaging
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.Firebase.CloudMessaging.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using HttpClient = FirebaseCoreSDK.HttpClients.HttpClient;

    // ReSharper disable once RedundantNameQualifier

    #endregion


    internal class CloudMessagingHttpClient : HttpClient, ICloudMessagingHttpClient
    {
        public CloudMessagingHttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
            : base(credentials, configuration, new Uri($"{configuration.CloudMessagingAuthority.TrimSlashes()}", UriKind.Absolute)) {}

        public async Task<PushMessageResponse> SendCloudMessageAsync(FirebasePushMessageEnvelope request)
        {
            var messageUri = new Uri($"/v1/projects/{Credentials.GetProjectId()}/messages:send");
            var dataAsString = await SendAsync(() => BuildRequestMessage(messageUri, request)).ConfigureAwait(false);

            var serializationOptions = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.DeserializeObject<PushMessageResponse>(dataAsString, serializationOptions);
        }

        private HttpRequestMessage BuildRequestMessage(Uri path, FirebasePushMessageEnvelope content)
        {
            var serializationOptions = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            var stringContent = JsonConvert.SerializeObject(content, serializationOptions);
            var jsonContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            var fullUri = GetFullAbsoluteUrl(path);

            var message = new HttpRequestMessage { RequestUri = fullUri, Method = HttpMethod.Post, Content = jsonContent };

            AddAuthHeaders(message);
            return message;
        }
    }
}