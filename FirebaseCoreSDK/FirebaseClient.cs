namespace FirebaseCoreSDK
{
    using System;

    using Configuration;

    using Firebase.Auth;
    using Firebase.Auth.ServiceAccounts;
    using Firebase.Storage;

    public sealed class FirebaseClient : IFirebaseClient
    {
        public FirebaseConfiguration Configuration { get; }
        public IFirebaseAuth Auth { get; private set; }
        //public IFirebaseDatabase Database { get; }
        //public IFirebaseNotification PushNotification { get; }
        public IFirebaseStorage Storage { get; private set; }

        public FirebaseClient(IServiceAccountCredentials credentials) :this(credentials, new FirebaseConfiguration())
        {

        }

        public FirebaseClient(IServiceAccountCredentials credentials, FirebaseServiceAccess requestedAccess) : this(credentials, new FirebaseConfiguration(requestedAccess))
        {
            
        }

        public FirebaseClient(IServiceAccountCredentials credentials, FirebaseConfiguration configuration)
        {
            Configuration = configuration;

            Initialize(credentials);
        }

        private void Initialize(IServiceAccountCredentials credentials)
        {
            var creds = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Auth = new FirebaseAuth(creds, Configuration);

            //if (FirebaseServiceAccess.DatabaseOnly == (configuration.RequestedAccess & FirebaseServiceAccess.DatabaseOnly))
            //    Database = new FirebaseDatabase(_auth, _credentials, configuration);

            if (FirebaseServiceAccess.StorageOnly == (Configuration.RequestedAccess & FirebaseServiceAccess.StorageOnly))
                Storage = new FirebaseStorage(creds, Configuration);

            //if (FirebaseServiceAccess.PushOnly == (configuration.RequestedAccess & FirebaseServiceAccess.PushOnly))
            //    PushNotification = new FirebaseNotification(_auth, _credentials, configuration);
        }

        #region IDisposable Methods
        public void Dispose(bool disposing)
        {
            if (!disposing) return;

            Auth.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FirebaseClient()
        {
            Dispose(false);
        }
        #endregion
    }
}