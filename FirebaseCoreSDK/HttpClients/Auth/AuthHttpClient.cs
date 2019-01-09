namespace FirebaseCoreSDK.HttpClients.Auth
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using Configuration;

    using Exceptions;

    using Extensions;

    using Firebase.Auth.AuthPayload;
    using Firebase.Auth.Models;
    using Firebase.Auth.ServiceAccounts;

    using Newtonsoft.Json;

    using Serializer;

    // ReSharper disable once RedundantNameQualifier
    using HttpClient = HttpClients.HttpClient;

    internal class AuthHttpClient : HttpClient, IAuthHttpClient
    {
        private readonly PayloadGenerator _jwtCustomTokenPayload;
        private readonly JwtAuthPayloadGenerator _jwtPayload;

        public AuthHttpClient(IServiceAccountCredentials credentials, FirebaseConfiguration configuration) : 
            base(credentials, configuration)
        {
            _jwtCustomTokenPayload = new CustomTokenPayloadGenerator(credentials, configuration);
            _jwtPayload = new JwtAuthPayloadGenerator(credentials, configuration);
        }

        public string CreateCustomToken(string userId)
        {
            var jwtPayload = _jwtCustomTokenPayload.GetPayload(new Dictionary<string, string>
            {
                { "uid", userId }
            });

            var rsaParams = Credentials.GetRSAParams();

            string jwtToken;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                jwtToken = Configuration.JwtTokenProvider.Encode(jwtPayload, rsa);
            }

            return jwtToken;
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


            var response = await PostAsync(Configuration.GoogleOAuthTokenPath, urlEncodedPayload).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

            if (response.Content == null)
            {
                throw new FirebaseHttpException("Authentication failed, empty response content from firebase server");
            }
            var strinRepresentation = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var serializationSettings = new JsonSerializerSettings { ContractResolver = new AccessTokenResolver() };
            var accessToken = JsonConvert.DeserializeObject<FirebaseAccessToken>(strinRepresentation, serializationSettings);
            if (accessToken == null)
            {
                throw new FirebaseHttpException("Authentication failed, unsupported content type was returned from firebase server");
            }
            return accessToken;
        }
    }
}