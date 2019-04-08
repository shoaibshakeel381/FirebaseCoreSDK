namespace FirebaseCoreSDK.HttpClients
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;

    #endregion


    internal abstract class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        protected readonly Uri Authority;
        protected readonly FirebaseSDKConfiguration Configuration;
        protected readonly IServiceAccountCredentials Credentials;

        protected HttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
            : this(credentials, configuration, null) {}

        protected HttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration, Uri authority)
        {
            Credentials = credentials;
            Configuration = configuration;
            Authority = authority;
        }

        public Uri GetAuthority() => Authority;

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

        protected async Task<string> SendAsync(Func<HttpRequestMessage> requestMessage)
        {
            var request = requestMessage();

            HttpResponseMessage response = null;

            try
            {
                await HttpRequestHelpers.LogOutgoingRequestInitiated(request, Configuration.Logger);
                response = await SendAsync(request).ConfigureAwait(false);
                await HttpRequestHelpers.LogOutgoingRequestCompleted(response, Configuration.Logger, null);
            }
            catch (Exception ex)
            {
                await HttpRequestHelpers.LogOutgoingRequestCompleted(response, Configuration.Logger, ex);
            }

            await HttpRequestHelpers.EnsureSuccessStatusCodeAsync(response, request, null).ConfigureAwait(false);


            if (response?.Content == null)
            {
                return null;
            }

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return dataAsString;
        }
    }
}