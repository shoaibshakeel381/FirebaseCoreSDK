namespace FirebaseCoreSDK
{
    using System;

    using Configuration;

    using Firebase.Auth;
    using Firebase.CloudMessaging;
    using Firebase.Database;
    using Firebase.Storage;

    public interface IFirebaseClient : IDisposable
    {
        FirebaseSDKConfiguration Configuration { get; }

        IFirebaseAuth Auth { get; }

        IFirebaseDatabase Database { get; }

        IFirebaseCloudMessaging CloudMessaging { get; }

        IFirebaseStorage Storage { get; }
    }
}
