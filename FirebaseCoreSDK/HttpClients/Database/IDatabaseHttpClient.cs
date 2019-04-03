namespace FirebaseCoreSDK.HttpClients.Database
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Firebase.Database;

    #endregion


    public interface IDatabaseHttpClient : IHttpClient
    {
        Task DeletePathAsync(Uri path);
        Task DeletePathAsync(string path);
        Task<T> GetFromPathAsync<T>(string path);
        Task<T> GetFromPathAsync<T>(Uri path);

        Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(Uri path) where T : KeyEntity;
        Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(string path) where T : KeyEntity;

        Task<string> PushToPathAsync<T>(Uri path, T content);
        Task<string> PushToPathAsync<T>(string path, T content);

        Task<T> SetToPathAsync<T>(string path, T content);
        Task<T> SetToPathAsync<T>(Uri path, T content);

        Task<string> UpdatePathAsync(string path, IDictionary<string, object> content);
        Task<T> UpdatePathAsync<T>(string path, IDictionary<string, object> content);
        Task<string> UpdatePathAsync(Uri path, IDictionary<string, object> content);
        Task<T> UpdatePathAsync<T>(Uri path, IDictionary<string, object> content);
    }
}