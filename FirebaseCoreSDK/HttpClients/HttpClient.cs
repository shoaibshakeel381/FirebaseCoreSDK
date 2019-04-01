namespace FirebaseCoreSDK.HttpClients
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Configuration;

    using Extensions;

    using Firebase.Auth.ServiceAccounts;

    internal abstract class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        protected readonly IServiceAccountCredentials Credentials;
        protected readonly FirebaseSDKConfiguration Configuration;
        protected readonly Uri Authority;

        protected HttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration) : this(credentials, configuration, null)
        {
        }

        protected HttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration, Uri authority)
        {
            Credentials = credentials;
            Configuration = configuration;
            Authority = authority;
        }

        public Uri GetAuthority()
        {
            return Authority;
        }

        protected async Task<string> SendAsync(Func<HttpRequestMessage> requestMessage)
        {
            var request = requestMessage();

            Configuration.Logger?.Info($"[{request.Method}] {request.RequestUri.AbsoluteUri}");

            var response = await SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

            if (response.Content == null)
                return null;

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Configuration.Logger?.Debug($"[RESPONSE] {dataAsString}");
            return dataAsString;
        }

        protected void AddAuthHeaders(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.AccessToken?.AccessToken);
        }

        protected Uri GetFullAbsoluteUrl(Uri uri)
        {
            if (Authority == null && !uri.IsAbsoluteUri)
            {
                throw new ArgumentOutOfRangeException(nameof(uri), "If Authority is missing uri should be absolute");
            }

            if (uri.IsAbsoluteUri)
            {
                return uri;
            }

            return Authority == null ? uri : Authority.Append(uri);
        }
    }
}