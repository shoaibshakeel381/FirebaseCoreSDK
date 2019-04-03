namespace FirebaseCoreSDK.HttpClients
{
    #region Namespace Imports

    using System;

    #endregion


    public interface IHttpClient : IDisposable
    {
        Uri GetAuthority();
    }
}