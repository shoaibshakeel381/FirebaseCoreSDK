namespace FirebaseCoreSDK.Firebase.Auth.Encryption
{
    #region Namespace Imports

    using System.Collections.Generic;
    using System.Security.Cryptography;

    #endregion


    public interface IJwtProvider
    {
        string Encode(IDictionary<string, string> payload, RSA privateKey);
    }
}