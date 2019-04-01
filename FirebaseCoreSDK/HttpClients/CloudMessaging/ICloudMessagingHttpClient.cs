namespace FirebaseCoreSDK.HttpClients.CloudMessaging
{
    using System.Threading.Tasks;

    using Firebase.CloudMessaging.Models;

    internal interface ICloudMessagingHttpClient : IHttpClient
    {
        Task<PushMessageResponse> SendCloudMessageAsync(FirebasePushMessageEnvelope request);
    }
}
