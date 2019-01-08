// ReSharper disable once CheckNamespace
namespace FirebaseCoreSDK.Firebase.Database
{
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        public static async Task DeleteAsync(this IDatabaseRef firebaseRef)
        {
            var databaseRef = ((DatabaseRef)firebaseRef);
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await databaseRef.HttpClient.DeletePathAsync(databaseRef.Path).ConfigureAwait(false);
        }
    }
}
