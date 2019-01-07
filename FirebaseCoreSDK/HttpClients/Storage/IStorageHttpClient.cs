namespace FirebaseCoreSDK.HttpClients.Storage
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal interface IStorageHttpClient : IHttpClient
    {
        Task<T> SendStorageRequestAsync<T>(Uri path, HttpMethod method);
    }
}
