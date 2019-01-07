namespace FirebaseCoreSDK.Configuration
{
    using System;
    using System.Collections.Generic;

    using Firebase.Auth.Encryption;
    using Firebase.Auth.Models;

    using HttpClients.Auth;

    public class FirebaseConfiguration
    {
        internal FirebaseServiceAccess RequestedAccess { get; }

        internal FirebaseAccessToken AccessToken { get; set; }

        internal IList<string> AllowedGoogleScopes
        {
            get
            {
                var scopeList = new List<string>() { "https://www.googleapis.com/auth/userinfo.email" };

                if (FirebaseServiceAccess.DatabaseOnly == (FirebaseServiceAccess.DatabaseOnly & RequestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/firebase");
                    scopeList.Add("https://www.googleapis.com/auth/firebase.database");
                }

                if (FirebaseServiceAccess.StorageOnly == (FirebaseServiceAccess.StorageOnly & RequestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/devstorage.full_control");
                }

                return scopeList;
            }
        }

        internal string GoogleScopeDelimiter => " ";

        internal IJwtProvider JwtTokenProvider => new JoseJwtProvider();

        public virtual Uri GoogleOAuthTokenPath => new Uri("https://www.googleapis.com/oauth2/v4/token");

        public virtual Uri CustomTokenPath => new Uri("https://identitytoolkit.googleapis.com/google.identity.identitytoolkit.v1.IdentityToolkit");

        // ReSharper disable once InconsistentNaming
        public virtual TimeSpan AccessTokenTTL => new TimeSpan(0, 4, 0);

        // ReSharper disable once InconsistentNaming
        public virtual TimeSpan CustomTokenTTL => new TimeSpan(0, 60, 0);

        public virtual string FirebaseHost => "firebaseio.com";

        public virtual string StorageBaseAuthority => "https://storage.googleapis.com";

        public virtual string StorageBaseAuthority2 => " https://www.googleapis.com/storage";

        public FirebaseConfiguration(): this(FirebaseServiceAccess.Full)
        {
        }

        public FirebaseConfiguration(FirebaseServiceAccess requestedAccess)
        {
            RequestedAccess = requestedAccess;
            AccessToken = new FirebaseAccessToken();
        }
    }

    [Flags]
    public enum FirebaseServiceAccess
    {
        DatabaseOnly = 0b00000001,
        StorageOnly = 0b00000010,
        PushOnly = 0b00000100,
        Full = 0b00000111
    }
}