namespace FirebaseCoreSDK.HttpClients.Auth
{
    #region Namespace Imports

    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Exceptions;
    using FirebaseCoreSDK.Firebase.Auth.AuthPayload;
    using FirebaseCoreSDK.Firebase.Auth.Models;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;

    using Newtonsoft.Json;

    #endregion


    internal class AuthHttpClient : FirebaseHttpClient, IAuthHttpClient
    {
        private readonly PayloadGenerator _jwtCustomTokenPayload;
        private readonly JwtAuthPayloadGenerator _jwtPayload;

        public AuthHttpClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
            : base(credentials, configuration)
        {
            _jwtCustomTokenPayload = new CustomTokenPayloadGenerator(credentials, configuration);
            _jwtPayload = new JwtAuthPayloadGenerator(credentials, configuration);
        }

        public async Task<FirebaseAccessToken> AuthenticateAsync()
        {
            var permissionPayload = GetPayload();
            var response = await SendAsync(() => PrepareAuthRequest(permissionPayload)).ConfigureAwait(false);
            return GetAccessToken(response);
        }

        public string CreateCustomToken(string userId)
        {
            var jwtPayload = _jwtCustomTokenPayload.GetPayload(new Dictionary<string, string> { { "uid", userId } });

            var rsaParams = Credentials.GetRSAParams();

            string jwtToken;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                jwtToken = Configuration.JwtTokenProvider.Encode(jwtPayload, rsa);
            }

            return jwtToken;
        }

        private static FirebaseAccessToken GetAccessToken(string response)
        {
            if (response == null)
            {
                throw new FirebaseHttpException("Authentication failed, empty response content from firebase server");
            }

            var accessToken = JsonConvert.DeserializeObject<FirebaseAccessToken>(response);

            if (accessToken == null)
            {
                throw new FirebaseHttpException("Authentication failed, unsupported content type was returned from firebase server");
            }

            return accessToken;
        }

        private IDictionary<string, string> GetPayload()
        {
            var jwtPayload = _jwtPayload.GetPayload();
            var rsaParams = Credentials.GetRSAParams();

            string jwtToken;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                jwtToken = Configuration.JwtTokenProvider.Encode(jwtPayload, rsa);
            }

            var permissionPayload = new PermissionAuthPayloadGenerator(jwtToken).GetPayload();
            return permissionPayload;
        }

        private HttpRequestMessage PrepareAuthRequest(IDictionary<string, string> permissionPayload)
        {
            var urlEncodedPayload = new FormUrlEncodedContent(permissionPayload);

            return new HttpRequestMessage(HttpMethod.Post, Configuration.GoogleOAuthTokenPath) { Content = urlEncodedPayload };
        }
    }
}