namespace FirebaseCoreSDK.Firebase.CloudMessaging
{
    using System;
    using System.Threading.Tasks;

    using Models;

    public interface IFirebaseCloudMessaging : IDisposable
    {
        Task<FirebasePushEnvelopeMessage> SendCloudMessageAsync(FirebasePushEnvelope request);
    }
}