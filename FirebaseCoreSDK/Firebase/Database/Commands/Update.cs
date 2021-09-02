// ReSharper disable once CheckNamespace

namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion


    public static partial class CommandExtensions
    {
        /// <exception cref="ArgumentNullException"><paramref name="content" /> is <see langword="null" /></exception>
        public static Task<string> UpdateAsync(this IDatabaseRef firebaseRef, IDictionary<string, object> content)
        {
            if (content == null || content.Count == 0)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var databaseRef = (DatabaseRef)firebaseRef;
            return databaseRef.HttpClient.UpdatePathAsync(databaseRef.Path, content);
        }

        /// <exception cref="ArgumentNullException"><paramref name="content" /> is <see langword="null" /></exception>
        public static Task<T> UpdateAsync<T>(this IDatabaseRef firebaseRef, IDictionary<string, object> content)
        {
            if (content == null || content.Count == 0)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var databaseRef = (DatabaseRef)firebaseRef;
            return databaseRef.HttpClient.UpdatePathAsync<T>(databaseRef.Path, content);
        }

        public static Task<T> UpdateWithKeyAsync<T>(this IDatabaseRef firebaseRef, IEnumerable<T> contentList) where T : class, IKeyEntity
        {
            if (contentList == null || !contentList.Any())
            {
                throw new ArgumentNullException(nameof(contentList));
            }

            var databaseRef = (DatabaseRef)firebaseRef;
            return databaseRef.HttpClient.UpdatePathWithKeyInjectedAsync(databaseRef.Path, contentList);
        }
    }
}