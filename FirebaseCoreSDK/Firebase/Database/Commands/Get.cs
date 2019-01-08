// ReSharper disable once CheckNamespace
namespace FirebaseCoreSDK.Firebase.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        public static Task<T> GetAsync<T>(this IDatabaseRef firebaseRef)
        {
            var databaseRef = (DatabaseRef)firebaseRef;
            var uri = new Uri(databaseRef.Path, UriKind.Relative);

            return databaseRef.HttpClient.GetFromPathAsync<T>(uri);
        }

        public static Task<T> GetAsync<T>(this IDatabaseRef firebaseRef, QueryBuilder queryBuilder)
        {
            var queryParams = $"?{queryBuilder.ToQueryString()}";
            var databaseRef = (DatabaseRef) firebaseRef;
            var uri = new Uri(databaseRef.Path + queryParams, UriKind.Relative);

            return databaseRef.HttpClient.GetFromPathAsync<T>(uri);
        }

        public static Task<IList<T>> GetWithKeyInjectedAsync<T>(this IDatabaseRef firebaseRef) where T : KeyEntity
        {
            var databaseRef = (DatabaseRef)firebaseRef;
            var uri = new Uri(databaseRef.Path, UriKind.Relative);

            return databaseRef.HttpClient.GetFromPathWithKeyInjectedAsync<T>(uri);
        }

        public static Task<IList<T>> GetWithKeyInjectedAsync<T>(this IDatabaseRef firebaseRef, QueryBuilder queryBuilder) where T : KeyEntity
        {
            var queryParams = $"?{queryBuilder.ToQueryString()}";
            var databaseRef = (DatabaseRef)firebaseRef;
            var uri = new Uri(databaseRef.Path + queryParams, UriKind.Relative);

            return databaseRef.HttpClient.GetFromPathWithKeyInjectedAsync<T>(uri);
        }
    }
}
