namespace FirebaseCoreSDK.Firebase.Auth
{
    using System;
    using System.Threading.Tasks;

    public interface IFirebaseAuth : IDisposable
    {
        string CreateCustomToken(string userId);

        Task AuthenticateAsync();
    }
}