namespace FirebaseCoreSDK.Firebase.Storage
{
    using System;
    using System.Threading.Tasks;

    using Models;

    public interface IFirebaseStorage
    {
        string GetPublicUrl(string path);

        string GetSignedUrl(SigningOption options);

        Task RemoveObjectAsync(string path);

        Task<(bool Result, Exception ex)> TryRemoveObjectAsync(string path);

        Task<ObjectMetadata> GetObjectMetaDataAsync(string path);

        Task MoveObjectAsync(string originPath, string destinationPath);

        Task<(bool Result, Exception ex)> TryMoveObjectAsync(string originPat, string destinationPath);
    }
}
