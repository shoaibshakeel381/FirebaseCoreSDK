namespace FirebaseCoreSDK
{
    using System;

    using Configuration;

    using Firebase.Auth;
    using Firebase.Auth.ServiceAccounts;
    using Firebase.CloudMessaging;
    using Firebase.Database;
    using Firebase.Storage;

    public sealed class FirebaseClient : IFirebaseClient
    {
        public FirebaseConfiguration Configuration { get; }
        public IFirebaseAuth Auth { get; private set; }
        public IFirebaseDatabase Database { get; private set; }
        public IFirebaseCloudMessaging CloudMessaging { get; private set; }
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

            if (FirebaseServiceAccess.Database == (Configuration.RequestedAccess & FirebaseServiceAccess.Database))
                Database = new FirebaseDatabase(creds, Configuration);

            if (FirebaseServiceAccess.Storage == (Configuration.RequestedAccess & FirebaseServiceAccess.Storage))
                Storage = new FirebaseStorage(creds, Configuration);

            if (FirebaseServiceAccess.CloudMessaging == (Configuration.RequestedAccess & FirebaseServiceAccess.CloudMessaging))
                CloudMessaging = new FirebaseCloudMessaging(creds, Configuration);
        }

        #region IDisposable Methods
        public void Dispose(bool disposing)
        {
            if (!disposing) return;

            Auth.Dispose();
            Database?.Dispose();
            Storage?.Dispose();
            CloudMessaging?.Dispose();
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