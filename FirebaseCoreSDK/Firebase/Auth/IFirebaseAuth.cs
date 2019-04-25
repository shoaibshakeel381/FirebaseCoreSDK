namespace FirebaseCoreSDK.Firebase.Auth
{
    #region Namespace Imports

    using System;
    using System.Threading.Tasks;

    #endregion


    public interface IFirebaseAuth
    {
        Task AuthenticateAsync();
        string CreateCustomToken(string userId);
    }
}