namespace FirebaseCoreSDK
{
    #region Namespace Imports

    using System;

    using FirebaseCoreSDK.Configuration;
    using FirebaseCoreSDK.Firebase.Auth;
    using FirebaseCoreSDK.Firebase.CloudMessaging;
    using FirebaseCoreSDK.Firebase.Database;
    using FirebaseCoreSDK.Firebase.Storage;

    #endregion


    public interface IFirebaseClient
    {
        IFirebaseAuth Auth { get; }

        IFirebaseCloudMessaging CloudMessaging { get; }
        
        FirebaseSDKConfiguration Configuration { get; }

        IFirebaseDatabase Database { get; }

        IFirebaseStorage Storage { get; }
    }
}