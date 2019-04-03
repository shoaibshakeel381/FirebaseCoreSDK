namespace FirebaseCoreSDK.Firebase.Auth.AuthPayload
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;

    #endregion


    public class CustomTokenPayloadGenerator : PayloadGenerator
    {
        private readonly FirebaseSDKConfiguration _configuration;
        private readonly IServiceAccountCredentials _creadentials;

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