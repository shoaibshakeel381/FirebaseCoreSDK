namespace FirebaseCoreSDK
{
    using System;

    using Configuration;

    using Firebase.Auth;

    public interface IFirebaseClient : IDisposable
    {
        FirebaseConfiguration Configuration { get; }

        IFirebaseAuth Auth { get; }

        //IFirebaseDatabase Database { get; }

        //IFirebaseNotification PushNotification { get; }

        //IFirebaseStorage Storage { get; }
    }
}
