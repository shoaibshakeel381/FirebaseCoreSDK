namespace FirebaseCoreSDK.Firebase.Storage
{
    #region Namespace Imports

    using System;
    using System.Threading.Tasks;

    using FirebaseCoreSDK.Firebase.Storage.Models;

    #endregion


    public interface IFirebaseStorage
    {
        Task<ObjectMetadata> GetObjectMetaDataAsync(string path);
        string GetPublicUrl(string path);

        string GetSignedUrl(SigningOption options);

        Task MoveObjectAsync(string originPath, string destinationPath);

        Task RemoveObjectAsync(string path);

        Task<(bool Result, Exception ex)> TryMoveObjectAsync(string originPat, string destinationPath);

        Task<(bool Result, Exception ex)> TryRemoveObjectAsync(string path);
    }
}