namespace FirebaseCoreSDK.HttpClients.CloudMessagign
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Configuration;

    using Extensions;

    using Firebase.Auth.ServiceAccounts;
    using Firebase.CloudMessaging.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // ReSharper disable once RedundantNameQualifier
    using HttpClient = HttpClients.HttpClient;

    internal class CloudMessagingHttpClient : HttpClient, ICloudMessagingHttpClient
    {
        public CloudMessagingHttpClient(IServiceAccountCredentials credentials, FirebaseConfiguration configuration) 
            : base(credentials, configuration, new Uri($"{configuration.CloudMessagingAuthority.TrimSlashes()}", UriKind.Absolute))
        {
            
        }

        public async Task<FirebasePushEnvelopeMessage> SendCloudMessageAsync(FirebasePushEnvelope request)
        {
            var messageUri = new Uri(Authority, $"/v1/projects/{Credentials.GetProjectId()}/messages:send");
            var data = JsonConvert.SerializeObject(request);

            var dataAsString = await SendAsync(() => BuildRequestMessage(messageUri, data)).ConfigureAwait(false);
            var serializationOptions = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.DeserializeObject<FirebasePushEnvelopeMessage>(dataAsString, serializationOptions);

        }

        private HttpRequestMessage BuildRequestMessage(Uri uri, string data = null)
        {
            StringContent content = null;

            if (data != null)
            {
                content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };

            AddAuthHeaders(requestMessage);
            return requestMessage;
        }
    }
}