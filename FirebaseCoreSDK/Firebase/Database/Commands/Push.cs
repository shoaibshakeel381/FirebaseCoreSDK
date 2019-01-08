// ReSharper disable once CheckNamespace
namespace FirebaseCoreSDK.Firebase.Database
{
    using System;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        /// <exception cref="ArgumentNullException"><paramref name="content"/> is <see langword="null"/></exception>
        public static Task<string> PushAsync<T>(this IDatabaseRef firebaseRef, T content)
        {
            if(content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var databaseRef = ((DatabaseRef)firebaseRef);
            return databaseRef.HttpClient.PushToPathAsync(databaseRef.Path, content);
        }
    }
}
