namespace FirebaseCoreSDK.Firebase.Auth.AuthPayload
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;

    #endregion


    public class JwtAuthPayloadGenerator : PayloadGenerator
    {
        private readonly FirebaseSDKConfiguration _configuration;
        private readonly IServiceAccountCredentials _creadentials;

        public JwtAuthPayloadGenerator(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            _creadentials = credentials;
            _configuration = configuration;
        }

        public sealed override IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            var iat = DateTime.Now.ToUnixSeconds();
            var exp = (DateTime.Now + _configuration.AccessTokenTTL).ToUnixSeconds();

            AddToPayload("scope", string.Join<string>(_configuration.GoogleScopeDelimiter, _configuration.AllowedGoogleScopes));
            AddToPayload("iss", _creadentials.GetServiceAccountEmail());
            AddToPayload("aud", _configuration.GoogleOAuthTokenPath);
            AddToPayload("exp", exp.ToString());
            AddToPayload("iat", iat.ToString());

            return base.GetPayload(additionalPayload);
        }
    }
}