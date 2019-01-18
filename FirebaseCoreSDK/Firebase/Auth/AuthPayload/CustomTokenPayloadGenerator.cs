namespace FirebaseCoreSDK.Firebase.Auth.AuthPayload
{
    using System;
    using System.Collections.Generic;

    using Configuration;

    using Extensions;

    using ServiceAccounts;

    public class CustomTokenPayloadGenerator : PayloadGenerator
    {
        private readonly IServiceAccountCredentials _creadentials;
        private readonly FirebaseSDKConfiguration _configuration;

        public CustomTokenPayloadGenerator(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            _creadentials = credentials;
            _configuration = configuration;
        }

        public sealed override IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            var iat = DateTime.Now.ToUnixSeconds();
            var exp = (DateTime.Now + _configuration.CustomTokenTTL).ToUnixSeconds();

            AddToPayload("iss", _creadentials.GetServiceAccountEmail());
            AddToPayload("sub", _creadentials.GetServiceAccountEmail());
            AddToPayload("aud", _configuration.CustomTokenPath);
            AddToPayload("iat", iat.ToString());
            AddToPayload("exp", exp.ToString());

            return base.GetPayload(additionalPayload);
        }
    }
}
