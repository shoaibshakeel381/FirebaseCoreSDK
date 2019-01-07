namespace FirebaseCoreSDK.Firebase.Auth.ServiceAccounts
{
    using System.Security.Cryptography;

    public interface IServiceAccountCredentials
    {
        // ReSharper disable once InconsistentNaming
        RSAParameters GetRSAParams();
        string GetServiceAccountEmail();
        string GetProjectId();
        string GetDefaultBucket();
    }
}