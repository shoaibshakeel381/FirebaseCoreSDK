namespace FirebaseCoreSDK.Firebase.Auth
{
    #region Namespace Imports

    using System;
    using System.Threading.Tasks;

    #endregion


    public interface IFirebaseAuth : IDisposable
    {
        Task AuthenticateAsync();
        string CreateCustomToken(string userId);
    }
}