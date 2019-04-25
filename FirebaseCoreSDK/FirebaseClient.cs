namespace FirebaseCoreSDK
{
    #region Namespace Imports

    using System;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Firebase.Auth;
    using FirebaseCoreSDK.Firebase.Auth.ServiceAccounts;
    using FirebaseCoreSDK.Firebase.CloudMessaging;
    using FirebaseCoreSDK.Firebase.Database;
    using FirebaseCoreSDK.Firebase.Storage;

    #endregion


    public sealed class FirebaseClient : IFirebaseClient
    {
        public FirebaseClient(FirebaseSDKConfiguration configuration)
            : this(configuration.Credentials, configuration) {}

        internal FirebaseClient(IServiceAccountCredentials credentials)
            : this(credentials, new FirebaseSDKConfiguration()) {}

        internal FirebaseClient(IServiceAccountCredentials credentials, FirebaseServiceAccess requestedAccess)
            : this(credentials, new FirebaseSDKConfiguration(requestedAccess)) {}

        internal FirebaseClient(IServiceAccountCredentials credentials, FirebaseSDKConfiguration configuration)
        {
            Configuration = configuration;

            Initialize(credentials);
        }

        public IFirebaseAuth Auth { get; private set; }
        public IFirebaseCloudMessaging CloudMessaging { get; private set; }
        public FirebaseSDKConfiguration Configuration { get; }
        public IFirebaseDatabase Database { get; private set; }
        public IFirebaseStorage Storage { get; private set; }

        private void Initialize(IServiceAccountCredentials credentials)
        {
            var creds = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Auth = new FirebaseAuth(creds, Configuration);

            if (FirebaseServiceAccess.Database == (Configuration.RequestedAccess & FirebaseServiceAccess.Database))
            {
                Database = new FirebaseDatabase(creds, Configuration);
            }

            if (FirebaseServiceAccess.Storage == (Configuration.RequestedAccess & FirebaseServiceAccess.Storage))
            {
                Storage = new FirebaseStorage(creds, Configuration);
            }

            if (FirebaseServiceAccess.CloudMessaging == (Configuration.RequestedAccess & FirebaseServiceAccess.CloudMessaging))
            {
                CloudMessaging = new FirebaseCloudMessaging(creds, Configuration);
            }
        }
    }
}