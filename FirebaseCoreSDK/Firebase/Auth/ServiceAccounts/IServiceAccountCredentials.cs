namespace FirebaseCoreSDK.Firebase.Auth.ServiceAccounts
{
    #region Namespace Imports

    using System.Security.Cryptography;

    #endregion


    public interface IServiceAccountCredentials
    {
        string GetDefaultBucket();
        string GetProjectId();

        // ReSharper disable once InconsistentNaming
        RSAParameters GetRSAParams();
        string GetServiceAccountEmail();
    }
}