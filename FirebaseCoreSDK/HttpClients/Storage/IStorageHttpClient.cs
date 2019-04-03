namespace FirebaseCoreSDK.HttpClients.Storage
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion


    internal interface IStorageHttpClient : IHttpClient
    {
        Task<T> SendStorageRequestAsync<T>(Uri path, HttpMethod method);
    }
}