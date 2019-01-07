namespace FirebaseCoreSDK.Firebase.Auth.Encryption
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public interface IJwtProvider
    {
        string Encode(IDictionary<string, string> payload, RSA privateKey);
    }
}
