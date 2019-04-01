namespace FirebaseCoreSDK.Firebase.CloudMessaging
{
    using System;
    using System.Threading.Tasks;

    using Models;

    public interface IFirebaseCloudMessaging : IDisposable
    {
        /// <summary>
        ///  Send Push Notifications
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dryRun">Only test the push message, don't actually send it</param>
        /// <returns></returns>
        Task<PushMessageResponse> SendCloudMessageAsync(FirebasePushMessage request, bool dryRun = false);
    }
}