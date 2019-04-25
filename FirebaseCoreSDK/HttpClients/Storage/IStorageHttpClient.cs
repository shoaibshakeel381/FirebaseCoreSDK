namespace FirebaseCoreSDK.HttpClients.Storage
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion


    internal interface IStorageHttpClient : IFirebaseHttpClient
    {
        Task<T> SendStorageRequestAsync<T>(Uri path, HttpMethod method);
    }
}