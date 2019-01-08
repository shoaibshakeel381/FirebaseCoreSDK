namespace FirebaseCoreSDK.HttpClients.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Firebase.Database;

    public interface IDatabaseHttpClient : IHttpClient
    {
        Task<T> GetFromPathAsync<T>(string path);
        Task<T> GetFromPathAsync<T>(Uri path);

        Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(Uri path) where T : KeyEntity;
        Task<IList<T>> GetFromPathWithKeyInjectedAsync<T>(string path) where T : KeyEntity;

        Task<T> SetToPathAsync<T>(string path, T content);
        Task<T> SetToPathAsync<T>(Uri path, T content);

        Task<string> PushToPathAsync<T>(Uri path, T content);
        Task<string> PushToPathAsync<T>(string path, T content);

        Task<object> UpdatePathAsync(string path, IDictionary<string, object> content);
        Task<object> UpdatePathAsync(Uri path, IDictionary<string, object> content);
    }
}
