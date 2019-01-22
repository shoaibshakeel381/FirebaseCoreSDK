namespace FirebaseCoreSDK.Configuration
{
    using System;
    using System.Collections.Generic;

    using Firebase.Auth.Encryption;
    using Firebase.Auth.Models;
    using Firebase.Auth.ServiceAccounts;

    using Logging;

    // ReSharper disable once InconsistentNaming
    public class FirebaseSDKConfiguration
    {
        public FirebaseSDKConfiguration(): this(FirebaseServiceAccess.Full)
        {

        }

        public FirebaseSDKConfiguration(FirebaseServiceAccess requestedAccess)
        {
            RequestedAccess = requestedAccess;
            AccessToken = new FirebaseAccessToken();
        }

        internal FirebaseServiceAccess RequestedAccess { get; }

        internal FirebaseAccessToken AccessToken { get; set; }

        internal IList<string> AllowedGoogleScopes
        {
            get
            {
                var scopeList = new List<string> { "https://www.googleapis.com/auth/userinfo.email" };

                if (FirebaseServiceAccess.Database == (FirebaseServiceAccess.Database & RequestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/firebase");
                    scopeList.Add("https://www.googleapis.com/auth/firebase.database");
                }

                if (FirebaseServiceAccess.Storage == (FirebaseServiceAccess.Storage & RequestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/devstorage.full_control");
                }

                if (FirebaseServiceAccess.CloudMessaging == (FirebaseServiceAccess.CloudMessaging & RequestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/firebase.messaging");
                }

                return scopeList;
            }
        }

        internal string GoogleScopeDelimiter => " ";

        internal IJwtProvider JwtTokenProvider => new JoseJwtProvider();

        /// <summary>
        /// TTL for one authentication session
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public TimeSpan AccessTokenTTL { get; set; } = new TimeSpan(0, 4, 0);

        /// <summary>
        /// TTL for custom token
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public TimeSpan CustomTokenTTL { get; set; } = new TimeSpan(0, 60, 0);

        /// <summary>
        /// Automatically authenticate all firebase API requests
        /// </summary>
        internal bool AutoAuthenticate => throw new NotImplementedException();

        /// <summary>
        /// Service account credentials.<br/>
        /// </summary>
        public IServiceAccountCredentials Credentials { get; set; }

        /// <summary>
        /// Custom Logger
        /// </summary>
        public IFirebaseLogger Logger { get; set; } = new FirebaseNullLogger();

        /// <summary>
        /// This is just the host name. Not actual URL.
        /// </summary>
        public string FirebaseHost { get; set; } = "firebaseio.com";

        /// <summary>
        /// Google OAuth URL
        /// </summary>
        public string GoogleOAuthTokenPath { get; set; } = "https://www.googleapis.com/oauth2/v4/token";

        public string CustomTokenPath { get; set; } = "https://identitytoolkit.googleapis.com/google.identity.identitytoolkit.v1.IdentityToolkit";

        /// <summary>
        /// Realtime Database service url
        /// </summary>
        public string RealtimeDatabaseAuthority
        {
            get => string.IsNullOrEmpty(_realtimeDatabaseAuthority) ? $"https://{Credentials.GetProjectId()}.{FirebaseHost}/" : _realtimeDatabaseAuthority;
            set => _realtimeDatabaseAuthority = value;
        }
        private string _realtimeDatabaseAuthority;

        /// <summary>
        /// Cloud Messaging service url
        /// </summary>
        public string CloudMessagingAuthority { get; set; } = "https://fcm.googleapis.com/";

        /// <summary>
        /// Storage service url
        /// </summary>
        public string StorageBaseAuthority { get; set; } = "https://storage.googleapis.com";

        /// <summary>
        /// Storage service url
        /// </summary>
        public string StorageBaseAuthority2 { get; set; } = " https://www.googleapis.com/storage";
    }

    [Flags]
    public enum FirebaseServiceAccess
    {
        Database = 0b00000001,
        Storage = 0b00000010,
        CloudMessaging = 0b00000100,
        Full = 0b00000111
    }
}