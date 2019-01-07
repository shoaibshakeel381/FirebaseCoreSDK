namespace FirebaseCoreSDK.HttpClients.Auth
{
    using System.Threading.Tasks;

    using Firebase.Auth.Models;

    internal interface IAuthHttpClient : IHttpClient
    {
        string CreateCustomToken(long userId);

        Task<FirebaseAccessToken> AuthenticateAsync();
    }
}
