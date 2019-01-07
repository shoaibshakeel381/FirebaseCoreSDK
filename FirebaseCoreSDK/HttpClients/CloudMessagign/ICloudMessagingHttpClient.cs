namespace FirebaseCoreSDK.HttpClients.CloudMessagign
{
    using System.Threading.Tasks;

    using Firebase.CloudMessaging.Models;

    internal interface ICloudMessagingHttpClient : IHttpClient
    {
        Task<FirebasePushEnvelopeMessage> SendCloudMessageAsync(FirebasePushEnvelope request);
    }
}
