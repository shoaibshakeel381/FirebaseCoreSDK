namespace FirebaseCoreSDK.HttpClients.Auth
{
    using System.Threading.Tasks;

    using Firebase.Auth.Models;

    internal interface IAuthHttpClient : IHttpClient
    {
        string CreateCustomToken(string userId);

        Task<FirebaseAccessToken> AuthenticateAsync();
    }
}
