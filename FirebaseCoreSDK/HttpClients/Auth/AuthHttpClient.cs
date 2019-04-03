namespace FirebaseCoreSDK.HttpClients.Auth
{
    #region Namespace Imports

    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Exceptions;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.AuthPayload;
    using FirebaseCoreSDK.Firebase.Auth.Models;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.HttpClients.Auth.Serializer;

    using Newtonsoft.Json;

    using HttpClient = FirebaseCoreSDK.HttpClients.HttpClient;

    // ReSharper disable once RedundantNameQualifier

    #endregion


    internal class AuthHttpClient : HttpClient, IAuthHttpClient
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
            var jwtPayload = _jwtPayload.GetPayload();
            var rsaParams = Credentials.GetRSAParams();

            string jwtToken;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                jwtToken = Configuration.JwtTokenProvider.Encode(jwtPayload, rsa);
            }

            var permissionPayload = new PermissionAuthPayloadGenerator(jwtToken).GetPayload();

            var urlEncodedPayload = new FormUrlEncodedContent(permissionPayload);

            Configuration.Logger?.Info($"[{HttpMethod.Post}] {Configuration.GoogleOAuthTokenPath}");
            var response = await PostAsync(Configuration.GoogleOAuthTokenPath, urlEncodedPayload).ConfigureAwait(false);
            await response.LogRequest(Configuration.Logger);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

            if (response.Content == null)
            {
                throw new FirebaseHttpException("Authentication failed, empty response content from firebase server");
            }

            var representation = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Configuration.Logger?.Debug($"[RESPONSE] {representation}");

            var serializationSettings = new JsonSerializerSettings { ContractResolver = new AccessTokenResolver() };
            var accessToken = JsonConvert.DeserializeObject<FirebaseAccessToken>(representation, serializationSettings);

            if (accessToken == null)
            {
                throw new FirebaseHttpException("Authentication failed, unsupported content type was returned from firebase server");
            }

            return accessToken;
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
    }
}